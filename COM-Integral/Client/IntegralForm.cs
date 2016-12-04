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
using MaxDerivative;

using MiddleRectsMethod;
using TrapMethod;
using SimpsonMethod;

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
            top_limit_textbox.Text = "pi/3";
            math_expression_textbox.Text = "sin(x)/cos(x)/(1+sin(x))";
            epsilon_textbox.Text = "1e-6";

            // init components
            mathParserComp = new Parser("x"); // set "x" letter as varable in string expressions

            SetStatus("Готово.");
        }


        void CalcualteIntegral() {
            ClearOutput();
            SetStatus("Идет вычисление...");

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
            } catch (Exception e) {
                SetStatus("Ошибка парсинга подынтегрального выражения.", isError:true);
                return;
            }

            

            // parse limits
            double b = mathParserComp.Parse(top_limit_textbox.Text).Evaluate();
            double a = mathParserComp.Parse(bottom_limit_textbox.Text).Evaluate();

            // parse eps
            double eps = double.Parse(epsilon_textbox.Text);

            // calculate max derivative on [a, b]
            var maxDerivativeComp = new MaxDerivativeCalculator(f, a, b);
            double max2ndDerivative = maxDerivativeComp.Calculate2nd();
            double max4thDerivative = maxDerivativeComp.Calculate4th();
            maxDerivativeComp.Dispose();

           // List<IDisposable> toDispose = new List<IDisposable>();

            // calculate integral with different methods
            if (applyMiddleRect) {
                var middleRectComp = new MiddleRectIntCalculator(a, b, max2ndDerivative, eps, f, new Dictionary<double, double>());
                WriteToResults("Средние прямоугольники:");
                WriteToResults(middleRectComp.Calculate().ToString());

                //toDispose.Add(middleRectComp);
            }

            if (applyTrap) {
                var trapComp = new TrapIntCalculator(a, b, max2ndDerivative, eps, f, new Dictionary<double, double>());
                WriteToResults("Трапеции:");
                WriteToResults(trapComp.Calculate().ToString());
                
               // toDispose.Add(trapComp);
            }

            if (applySimpson) {
                var simpsonComp = new SimpsonIntCalculator(a, b, max4thDerivative, eps, f, new Dictionary<double, double>());
                WriteToResults("Симпсон:");
                WriteToResults(simpsonComp.Calculate().ToString());
                
               // toDispose.Add(simpsonComp);
            }


            /*foreach (var d in toDispose) {
                d.Dispose();
            }*/


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
                //double.Parse(top_limit_textbox.Text);
                //double.Parse(bottom_limit_textbox.Text);
                mathParserComp.Parse(top_limit_textbox.Text);
                mathParserComp.Parse(bottom_limit_textbox.Text);
            } catch {
                return "Ошибка в пределах интегралов";
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


        void ClearOutput() {
            results_textbox.Text = "";
        }

        void WriteToResults(string message, bool clearBeforeOutput = false) {
            if (clearBeforeOutput) {
                ClearOutput();
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
