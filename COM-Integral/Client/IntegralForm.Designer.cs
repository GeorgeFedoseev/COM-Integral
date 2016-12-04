namespace Client {
    partial class IntegralForm {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegralForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.top_limit_textbox = new System.Windows.Forms.TextBox();
            this.bottom_limit_textbox = new System.Windows.Forms.TextBox();
            this.math_expression_textbox = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.status_label = new System.Windows.Forms.Label();
            this.method_checkbox_middleRect = new System.Windows.Forms.CheckBox();
            this.method_checkbox_trap = new System.Windows.Forms.CheckBox();
            this.method_checkbox_simpson = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.results_textbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.integrate_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 99);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // top_limit_textbox
            // 
            this.top_limit_textbox.Location = new System.Drawing.Point(57, 12);
            this.top_limit_textbox.Name = "top_limit_textbox";
            this.top_limit_textbox.Size = new System.Drawing.Size(47, 20);
            this.top_limit_textbox.TabIndex = 1;
            // 
            // bottom_limit_textbox
            // 
            this.bottom_limit_textbox.Location = new System.Drawing.Point(25, 140);
            this.bottom_limit_textbox.Name = "bottom_limit_textbox";
            this.bottom_limit_textbox.Size = new System.Drawing.Size(47, 20);
            this.bottom_limit_textbox.TabIndex = 2;
            // 
            // math_expression_textbox
            // 
            this.math_expression_textbox.Location = new System.Drawing.Point(76, 76);
            this.math_expression_textbox.Name = "math_expression_textbox";
            this.math_expression_textbox.Size = new System.Drawing.Size(155, 20);
            this.math_expression_textbox.TabIndex = 3;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(237, 66);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(47, 40);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // status_label
            // 
            this.status_label.AutoSize = true;
            this.status_label.Location = new System.Drawing.Point(22, 313);
            this.status_label.Name = "status_label";
            this.status_label.Size = new System.Drawing.Size(35, 13);
            this.status_label.TabIndex = 5;
            this.status_label.Text = "status";
            // 
            // method_checkbox_middleRect
            // 
            this.method_checkbox_middleRect.AutoSize = true;
            this.method_checkbox_middleRect.Location = new System.Drawing.Point(24, 201);
            this.method_checkbox_middleRect.Name = "method_checkbox_middleRect";
            this.method_checkbox_middleRect.Size = new System.Drawing.Size(156, 17);
            this.method_checkbox_middleRect.TabIndex = 6;
            this.method_checkbox_middleRect.Text = "Средние прямоугольники";
            this.method_checkbox_middleRect.UseVisualStyleBackColor = true;
            this.method_checkbox_middleRect.CheckedChanged += new System.EventHandler(this.method_checkbox_middleRect_CheckedChanged);
            // 
            // method_checkbox_trap
            // 
            this.method_checkbox_trap.AutoSize = true;
            this.method_checkbox_trap.Location = new System.Drawing.Point(24, 224);
            this.method_checkbox_trap.Name = "method_checkbox_trap";
            this.method_checkbox_trap.Size = new System.Drawing.Size(75, 17);
            this.method_checkbox_trap.TabIndex = 7;
            this.method_checkbox_trap.Text = "Трапеции";
            this.method_checkbox_trap.UseVisualStyleBackColor = true;
            this.method_checkbox_trap.CheckedChanged += new System.EventHandler(this.method_checkbox_trap_CheckedChanged);
            // 
            // method_checkbox_simpson
            // 
            this.method_checkbox_simpson.AutoSize = true;
            this.method_checkbox_simpson.Location = new System.Drawing.Point(24, 247);
            this.method_checkbox_simpson.Name = "method_checkbox_simpson";
            this.method_checkbox_simpson.Size = new System.Drawing.Size(71, 17);
            this.method_checkbox_simpson.TabIndex = 8;
            this.method_checkbox_simpson.Text = "Симпсон";
            this.method_checkbox_simpson.UseVisualStyleBackColor = true;
            this.method_checkbox_simpson.CheckedChanged += new System.EventHandler(this.method_checkbox_simpson_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Выберите метод интегрирования:";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(312, 140);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(77, 20);
            this.textBox4.TabIndex = 10;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(244, 130);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(36, 40);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(276, 141);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 19);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // results_textbox
            // 
            this.results_textbox.BackColor = System.Drawing.Color.White;
            this.results_textbox.Location = new System.Drawing.Point(244, 204);
            this.results_textbox.Name = "results_textbox";
            this.results_textbox.ReadOnly = true;
            this.results_textbox.Size = new System.Drawing.Size(250, 105);
            this.results_textbox.TabIndex = 13;
            this.results_textbox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(241, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Результаты:";
            // 
            // integrate_button
            // 
            this.integrate_button.Location = new System.Drawing.Point(24, 275);
            this.integrate_button.Name = "integrate_button";
            this.integrate_button.Size = new System.Drawing.Size(100, 23);
            this.integrate_button.TabIndex = 15;
            this.integrate_button.Text = "Интегрировать!";
            this.integrate_button.UseVisualStyleBackColor = true;
            this.integrate_button.Click += new System.EventHandler(this.integrate_button_Click);
            // 
            // IntegralForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 335);
            this.Controls.Add(this.integrate_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.results_textbox);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.method_checkbox_simpson);
            this.Controls.Add(this.method_checkbox_trap);
            this.Controls.Add(this.method_checkbox_middleRect);
            this.Controls.Add(this.status_label);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.math_expression_textbox);
            this.Controls.Add(this.bottom_limit_textbox);
            this.Controls.Add(this.top_limit_textbox);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "IntegralForm";
            this.Text = "Integral Calculator";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox top_limit_textbox;
        private System.Windows.Forms.TextBox bottom_limit_textbox;
        private System.Windows.Forms.TextBox math_expression_textbox;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label status_label;
        private System.Windows.Forms.CheckBox method_checkbox_middleRect;
        private System.Windows.Forms.CheckBox method_checkbox_trap;
        private System.Windows.Forms.CheckBox method_checkbox_simpson;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.RichTextBox results_textbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button integrate_button;
    }
}

