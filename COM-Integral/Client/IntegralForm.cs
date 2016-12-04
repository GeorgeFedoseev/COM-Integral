using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// components
using MathParser;

namespace Client {
    public partial class IntegralForm : Form {

        bool applyMiddleRect, applyTrap, applySimpson;
        Parser mathParserComp;

        public IntegralForm() {
            InitializeComponent();

            // init variables
            applyMiddleRect = method_checkbox_middleRect.Checked = true;
            applyTrap = applySimpson = false;
            bottom_limit_textbox.Text = "0";
            top_limit_textbox.Text = "1";
            math_expression_textbox.Text = "sin(x)";
            epsilon_textbox.Text = "1e-6";

            // init components
            mathParserComp = new Parser("x"); // set "x" letter as varable in string expressions
        }


        void CalcualteIntegral() {
            // validate form
            var error = GetInputFieldsInvalidError();
            if (error != null) {
                SetStatus(error, isError:true);
                return;
            }

            // parse math expression to func            
            Func<double, double> f;
            try {
                f = mathParserComp.Parse(math_expression_textbox.Text).ToExpression<Func<double, double>>().Compile();

                WriteToResults(f.Invoke(Math.PI / 2).ToString());

            } catch (Exception e) {
                SetStatus("Ошибка парсинга подынтегрального выражения.", isError:true);
                return;
            }

            

            


            SetStatus("Готово.");
        }

        string GetInputFieldsInvalidError() {
            // check math expression
            if (math_expression_textbox.Text == "") {
                return "Вы не ввели подынтегральное выражение.";
            }
            // check limits
            if (top_limit_textbox.Text == "") {
                return "Вы не ввели верхний предел.";
            }
                      

            if (bottom_limit_textbox.Text == "") {
                return "Вы не ввели нижний предел.";
            }

            try {
                double.Parse(top_limit_textbox.Text);
                double.Parse(bottom_limit_textbox.Text);
            } catch {
                return "Пределы должены быть числами";
            }

            // check epsilon
            if (epsilon_textbox.Text == "") {
                return "Вы не ввели точность вычислений эпсилон.";
            }

            try {
                if (double.Parse(epsilon_textbox.Text) <= 0) {
                    return "Эпсилон должно быть больше нуля";
                }
            } catch {
                return "Эпсилон должно быть числом";
            }

            

            // check at least one method selected
            if (applyMiddleRect == false && applyTrap == false && applySimpson == false) {
                return "Не выбрано ни одного метода интегрирования.";
            }           

            return null;
        }


        void WriteToResults(string message, bool clearBeforeOutput = false) {
            if (clearBeforeOutput) {
                results_textbox.Text = "";
            }

            results_textbox.Text += message+ "\n";
        }

        void SetStatus(string message, bool isError = false) {
            status_label.Text = message;
            status_label.ForeColor = isError ? Color.Red : Color.Black;
        }



        // EVENTS

        // integrate button
        private void integrate_button_Click(object sender, EventArgs e) {
            CalcualteIntegral();
        }

        // checkbox events
        private void method_checkbox_middleRect_CheckedChanged(object sender, EventArgs e) {
            applyMiddleRect = method_checkbox_middleRect.Checked;
        }

        private void method_checkbox_trap_CheckedChanged(object sender, EventArgs e) {
            applyTrap = method_checkbox_trap.Checked;
        }

        private void method_checkbox_simpson_CheckedChanged(object sender, EventArgs e) {
            applySimpson = method_checkbox_simpson.Checked;
        }
    }
}
