namespace PLForm
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTest = new System.Windows.Forms.Button();
            this.ucH1 = new PLForm.UcH();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkAutoManual = new System.Windows.Forms.CheckBox();
            this.txtZtPlcStatus = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.MintCream;
            this.panel1.Controls.Add(this.txtZtPlcStatus);
            this.panel1.Controls.Add(this.chkAutoManual);
            this.panel1.Controls.Add(this.btnStart);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(771, 50);
            this.panel1.TabIndex = 1;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(3, 12);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // ucH1
            // 
            this.ucH1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucH1.Location = new System.Drawing.Point(0, 50);
            this.ucH1.Name = "ucH1";
            this.ucH1.Size = new System.Drawing.Size(771, 221);
            this.ucH1.TabIndex = 0;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(684, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chkAutoManual
            // 
            this.chkAutoManual.AutoSize = true;
            this.chkAutoManual.Location = new System.Drawing.Point(561, 3);
            this.chkAutoManual.Name = "chkAutoManual";
            this.chkAutoManual.Size = new System.Drawing.Size(84, 16);
            this.chkAutoManual.TabIndex = 2;
            this.chkAutoManual.Text = "AutoManual";
            this.chkAutoManual.UseVisualStyleBackColor = true;
            // 
            // txtZtPlcStatus
            // 
            this.txtZtPlcStatus.Location = new System.Drawing.Point(464, 12);
            this.txtZtPlcStatus.Name = "txtZtPlcStatus";
            this.txtZtPlcStatus.Size = new System.Drawing.Size(37, 21);
            this.txtZtPlcStatus.TabIndex = 3;
            this.txtZtPlcStatus.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 271);
            this.Controls.Add(this.ucH1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UcH ucH1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chkAutoManual;
        private System.Windows.Forms.TextBox txtZtPlcStatus;
    }
}

