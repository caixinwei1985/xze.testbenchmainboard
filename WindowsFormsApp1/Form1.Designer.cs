namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.btFan2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btFan1 = new System.Windows.Forms.Button();
            this.btVout = new System.Windows.Forms.Button();
            this.btVcc = new System.Windows.Forms.Button();
            this.btLed = new System.Windows.Forms.Button();
            this.btBuzzer = new System.Windows.Forms.Button();
            this.btLamp = new System.Windows.Forms.Button();
            this.btLaser = new System.Windows.Forms.Button();
            this.btHPO1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.chkXSW0 = new System.Windows.Forms.CheckBox();
            this.chkXSW1 = new System.Windows.Forms.CheckBox();
            this.chkXSW2 = new System.Windows.Forms.CheckBox();
            this.chkYSW0 = new System.Windows.Forms.CheckBox();
            this.chkYSW1 = new System.Windows.Forms.CheckBox();
            this.chkYSW2 = new System.Windows.Forms.CheckBox();
            this.chkZSW0 = new System.Windows.Forms.CheckBox();
            this.chkZSW1 = new System.Windows.Forms.CheckBox();
            this.chkZSW2 = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.nudSteps = new System.Windows.Forms.NumericUpDown();
            this.nudSpeed = new System.Windows.Forms.NumericUpDown();
            this.nudAcc = new System.Windows.Forms.NumericUpDown();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.axisBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.chkNeg = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btISP = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(173, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Moto Move";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btFan2
            // 
            this.btFan2.Location = new System.Drawing.Point(254, 7);
            this.btFan2.Name = "btFan2";
            this.btFan2.Size = new System.Drawing.Size(75, 23);
            this.btFan2.TabIndex = 5;
            this.btFan2.Text = "Fan2";
            this.btFan2.UseVisualStyleBackColor = true;
            this.btFan2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(254, 268);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "HPO2";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btFan1
            // 
            this.btFan1.Location = new System.Drawing.Point(254, 36);
            this.btFan1.Name = "btFan1";
            this.btFan1.Size = new System.Drawing.Size(75, 23);
            this.btFan1.TabIndex = 5;
            this.btFan1.Text = "Fan1";
            this.btFan1.UseVisualStyleBackColor = true;
            this.btFan1.Click += new System.EventHandler(this.btFan1_Click);
            // 
            // btVout
            // 
            this.btVout.Location = new System.Drawing.Point(254, 65);
            this.btVout.Name = "btVout";
            this.btVout.Size = new System.Drawing.Size(75, 23);
            this.btVout.TabIndex = 5;
            this.btVout.Text = "Vout";
            this.btVout.UseVisualStyleBackColor = true;
            this.btVout.Click += new System.EventHandler(this.btVout_Click);
            // 
            // btVcc
            // 
            this.btVcc.Location = new System.Drawing.Point(254, 94);
            this.btVcc.Name = "btVcc";
            this.btVcc.Size = new System.Drawing.Size(75, 23);
            this.btVcc.TabIndex = 5;
            this.btVcc.Text = "Vcc";
            this.btVcc.UseVisualStyleBackColor = true;
            this.btVcc.Click += new System.EventHandler(this.btVcc_Click);
            // 
            // btLed
            // 
            this.btLed.Location = new System.Drawing.Point(254, 123);
            this.btLed.Name = "btLed";
            this.btLed.Size = new System.Drawing.Size(75, 23);
            this.btLed.TabIndex = 5;
            this.btLed.Text = "Led";
            this.btLed.UseVisualStyleBackColor = true;
            this.btLed.Click += new System.EventHandler(this.btLed_Click);
            // 
            // btBuzzer
            // 
            this.btBuzzer.Location = new System.Drawing.Point(254, 152);
            this.btBuzzer.Name = "btBuzzer";
            this.btBuzzer.Size = new System.Drawing.Size(75, 23);
            this.btBuzzer.TabIndex = 5;
            this.btBuzzer.Text = "Buzzer";
            this.btBuzzer.UseVisualStyleBackColor = true;
            this.btBuzzer.Click += new System.EventHandler(this.btBuzzer_Click);
            // 
            // btLamp
            // 
            this.btLamp.Location = new System.Drawing.Point(254, 181);
            this.btLamp.Name = "btLamp";
            this.btLamp.Size = new System.Drawing.Size(75, 23);
            this.btLamp.TabIndex = 5;
            this.btLamp.Text = "Lamp";
            this.btLamp.UseVisualStyleBackColor = true;
            this.btLamp.Click += new System.EventHandler(this.btLamp_Click);
            // 
            // btLaser
            // 
            this.btLaser.Location = new System.Drawing.Point(254, 210);
            this.btLaser.Name = "btLaser";
            this.btLaser.Size = new System.Drawing.Size(75, 23);
            this.btLaser.TabIndex = 5;
            this.btLaser.Text = "Laser";
            this.btLaser.UseVisualStyleBackColor = true;
            this.btLaser.Click += new System.EventHandler(this.btLaser_Click);
            // 
            // btHPO1
            // 
            this.btHPO1.Location = new System.Drawing.Point(254, 239);
            this.btHPO1.Name = "btHPO1";
            this.btHPO1.Size = new System.Drawing.Size(75, 23);
            this.btHPO1.TabIndex = 5;
            this.btHPO1.Text = "HPO1";
            this.btHPO1.UseVisualStyleBackColor = true;
            this.btHPO1.Click += new System.EventHandler(this.btHPO1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(173, 36);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Moto Reset";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(173, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Acc Test";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chkXSW0
            // 
            this.chkXSW0.AutoSize = true;
            this.chkXSW0.Location = new System.Drawing.Point(342, 10);
            this.chkXSW0.Name = "chkXSW0";
            this.chkXSW0.Size = new System.Drawing.Size(48, 16);
            this.chkXSW0.TabIndex = 8;
            this.chkXSW0.Text = "XSW0";
            this.chkXSW0.UseVisualStyleBackColor = true;
            // 
            // chkXSW1
            // 
            this.chkXSW1.AutoSize = true;
            this.chkXSW1.Location = new System.Drawing.Point(342, 33);
            this.chkXSW1.Name = "chkXSW1";
            this.chkXSW1.Size = new System.Drawing.Size(48, 16);
            this.chkXSW1.TabIndex = 8;
            this.chkXSW1.Text = "XSW1";
            this.chkXSW1.UseVisualStyleBackColor = true;
            // 
            // chkXSW2
            // 
            this.chkXSW2.AutoSize = true;
            this.chkXSW2.Location = new System.Drawing.Point(342, 55);
            this.chkXSW2.Name = "chkXSW2";
            this.chkXSW2.Size = new System.Drawing.Size(48, 16);
            this.chkXSW2.TabIndex = 8;
            this.chkXSW2.Text = "XSW2";
            this.chkXSW2.UseVisualStyleBackColor = true;
            // 
            // chkYSW0
            // 
            this.chkYSW0.AutoSize = true;
            this.chkYSW0.Location = new System.Drawing.Point(426, 10);
            this.chkYSW0.Name = "chkYSW0";
            this.chkYSW0.Size = new System.Drawing.Size(48, 16);
            this.chkYSW0.TabIndex = 8;
            this.chkYSW0.Text = "YSW0";
            this.chkYSW0.UseVisualStyleBackColor = true;
            // 
            // chkYSW1
            // 
            this.chkYSW1.AutoSize = true;
            this.chkYSW1.Location = new System.Drawing.Point(426, 33);
            this.chkYSW1.Name = "chkYSW1";
            this.chkYSW1.Size = new System.Drawing.Size(48, 16);
            this.chkYSW1.TabIndex = 8;
            this.chkYSW1.Text = "YSW1";
            this.chkYSW1.UseVisualStyleBackColor = true;
            // 
            // chkYSW2
            // 
            this.chkYSW2.AutoSize = true;
            this.chkYSW2.Location = new System.Drawing.Point(426, 55);
            this.chkYSW2.Name = "chkYSW2";
            this.chkYSW2.Size = new System.Drawing.Size(48, 16);
            this.chkYSW2.TabIndex = 8;
            this.chkYSW2.Text = "YSW2";
            this.chkYSW2.UseVisualStyleBackColor = true;
            // 
            // chkZSW0
            // 
            this.chkZSW0.AutoSize = true;
            this.chkZSW0.Location = new System.Drawing.Point(510, 10);
            this.chkZSW0.Name = "chkZSW0";
            this.chkZSW0.Size = new System.Drawing.Size(78, 16);
            this.chkZSW0.TabIndex = 8;
            this.chkZSW0.Text = "checkBox1";
            this.chkZSW0.UseVisualStyleBackColor = true;
            // 
            // chkZSW1
            // 
            this.chkZSW1.AutoSize = true;
            this.chkZSW1.Location = new System.Drawing.Point(510, 33);
            this.chkZSW1.Name = "chkZSW1";
            this.chkZSW1.Size = new System.Drawing.Size(78, 16);
            this.chkZSW1.TabIndex = 8;
            this.chkZSW1.Text = "checkBox1";
            this.chkZSW1.UseVisualStyleBackColor = true;
            // 
            // chkZSW2
            // 
            this.chkZSW2.AutoSize = true;
            this.chkZSW2.Location = new System.Drawing.Point(510, 55);
            this.chkZSW2.Name = "chkZSW2";
            this.chkZSW2.Size = new System.Drawing.Size(78, 16);
            this.chkZSW2.TabIndex = 8;
            this.chkZSW2.Text = "checkBox1";
            this.chkZSW2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(173, 93);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 9;
            this.button5.Text = "button5";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // nudSteps
            // 
            this.nudSteps.Location = new System.Drawing.Point(47, 10);
            this.nudSteps.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudSteps.Name = "nudSteps";
            this.nudSteps.Size = new System.Drawing.Size(120, 21);
            this.nudSteps.TabIndex = 10;
            this.nudSteps.Value = new decimal(new int[] {
            400000,
            0,
            0,
            0});
            // 
            // nudSpeed
            // 
            this.nudSpeed.Location = new System.Drawing.Point(47, 37);
            this.nudSpeed.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nudSpeed.Name = "nudSpeed";
            this.nudSpeed.Size = new System.Drawing.Size(120, 21);
            this.nudSpeed.TabIndex = 10;
            this.nudSpeed.Value = new decimal(new int[] {
            40000,
            0,
            0,
            0});
            // 
            // nudAcc
            // 
            this.nudAcc.Location = new System.Drawing.Point(47, 64);
            this.nudAcc.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudAcc.Name = "nudAcc";
            this.nudAcc.Size = new System.Drawing.Size(120, 21);
            this.nudAcc.TabIndex = 10;
            this.nudAcc.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(47, 91);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(52, 20);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // axisBindingSource
            // 
            this.axisBindingSource.DataSource = typeof(Xze.TestBench.Mainboard.Axis);
            // 
            // chkNeg
            // 
            this.chkNeg.AutoSize = true;
            this.chkNeg.Location = new System.Drawing.Point(105, 93);
            this.chkNeg.Name = "chkNeg";
            this.chkNeg.Size = new System.Drawing.Size(72, 16);
            this.chkNeg.TabIndex = 12;
            this.chkNeg.Text = "Negitive";
            this.chkNeg.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "Steps";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "Speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "Acc";
            // 
            // btISP
            // 
            this.btISP.Location = new System.Drawing.Point(173, 123);
            this.btISP.Name = "btISP";
            this.btISP.Size = new System.Drawing.Size(75, 23);
            this.btISP.TabIndex = 14;
            this.btISP.Text = "ISP";
            this.btISP.UseVisualStyleBackColor = true;
            this.btISP.Click += new System.EventHandler(this.btISP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btISP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkNeg);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.nudAcc);
            this.Controls.Add(this.nudSpeed);
            this.Controls.Add(this.nudSteps);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chkZSW2);
            this.Controls.Add(this.chkZSW1);
            this.Controls.Add(this.chkYSW2);
            this.Controls.Add(this.chkYSW1);
            this.Controls.Add(this.chkZSW0);
            this.Controls.Add(this.chkXSW2);
            this.Controls.Add(this.chkYSW0);
            this.Controls.Add(this.chkXSW1);
            this.Controls.Add(this.chkXSW0);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btHPO1);
            this.Controls.Add(this.btLaser);
            this.Controls.Add(this.btLamp);
            this.Controls.Add(this.btBuzzer);
            this.Controls.Add(this.btLed);
            this.Controls.Add(this.btVcc);
            this.Controls.Add(this.btVout);
            this.Controls.Add(this.btFan1);
            this.Controls.Add(this.btFan2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudSteps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAcc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axisBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btFan2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btFan1;
        private System.Windows.Forms.Button btVout;
        private System.Windows.Forms.Button btVcc;
        private System.Windows.Forms.Button btLed;
        private System.Windows.Forms.Button btBuzzer;
        private System.Windows.Forms.Button btLamp;
        private System.Windows.Forms.Button btLaser;
        private System.Windows.Forms.Button btHPO1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chkXSW0;
        private System.Windows.Forms.CheckBox chkXSW1;
        private System.Windows.Forms.CheckBox chkXSW2;
        private System.Windows.Forms.CheckBox chkYSW0;
        private System.Windows.Forms.CheckBox chkYSW1;
        private System.Windows.Forms.CheckBox chkYSW2;
        private System.Windows.Forms.CheckBox chkZSW0;
        private System.Windows.Forms.CheckBox chkZSW1;
        private System.Windows.Forms.CheckBox chkZSW2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.NumericUpDown nudSteps;
        private System.Windows.Forms.NumericUpDown nudSpeed;
        private System.Windows.Forms.NumericUpDown nudAcc;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.BindingSource axisBindingSource;
        private System.Windows.Forms.CheckBox chkNeg;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btISP;
    }
}

