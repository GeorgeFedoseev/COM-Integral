using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client {
    public partial class IntegralForm : Form {

        bool applyMiddleRect, applyTrap, applySimpson;

        public IntegralForm() {
            InitializeComponent();

            // init variables
            applyMiddleRect = applyTrap = applySimpson = false;
        }


        void CalcualteIntegral() {
            // validate form
            var error = GetInputFieldsInvalidError();
            if (error != null) {
                SetStatus(error, isError:true);
                return;
            }


            SetStatus("Готово.");
        }

        string GetInputFieldsInvalidError() {
            // check math expression
            // check limits
            // check epsilon
            // check at least one method selected

            if (applyMiddleRect == false && applyTrap == false && applySimpson == false) {
                return "Не выбрано ни одного метода интегрирования.";
            }           

            return null;
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
