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
            this.tbReg = new System.Windows.Forms.TextBox();
            this.tbVal = new System.Windows.Forms.TextBox();
            this.btSend = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.chkSet = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbReg
            // 
            this.tbReg.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.tbReg.Location = new System.Drawing.Point(12, 12);
            this.tbReg.Name = "tbReg";
            this.tbReg.Size = new System.Drawing.Size(100, 21);
            this.tbReg.TabIndex = 0;
            this.tbReg.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tbVal
            // 
            this.tbVal.Location = new System.Drawing.Point(12, 39);
            this.tbVal.Name = "tbVal";
            this.tbVal.Size = new System.Drawing.Size(100, 21);
            this.tbVal.TabIndex = 1;
            // 
            // btSend
            // 
            this.btSend.Location = new System.Drawing.Point(118, 10);
            this.btSend.Name = "btSend";
            this.btSend.Size = new System.Drawing.Size(75, 50);
            this.btSend.TabIndex = 2;
            this.btSend.Text = "button1";
            this.btSend.UseVisualStyleBackColor = true;
            this.btSend.Click += new System.EventHandler(this.btSend_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(215, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chkSet
            // 
            this.chkSet.AutoSize = true;
            this.chkSet.Location = new System.Drawing.Point(12, 66);
            this.chkSet.Name = "chkSet";
            this.chkSet.Size = new System.Drawing.Size(78, 16);
            this.chkSet.TabIndex = 4;
            this.chkSet.Text = "checkBox1";
            this.chkSet.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(306, 39);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(690, 282);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkSet);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btSend);
            this.Controls.Add(this.tbVal);
            this.Controls.Add(this.tbReg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbReg;
        private System.Windows.Forms.TextBox tbVal;
        private System.Windows.Forms.Button btSend;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox chkSet;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

