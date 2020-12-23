namespace PLC {
    partial class frmOpcValues {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.dgvOpcValues = new System.Windows.Forms.DataGridView();
            this.chName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpcValues)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOpcValues
            // 
            this.dgvOpcValues.AllowUserToDeleteRows = false;
            this.dgvOpcValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpcValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chName,
            this.chValue});
            this.dgvOpcValues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOpcValues.Location = new System.Drawing.Point(0, 0);
            this.dgvOpcValues.Name = "dgvOpcValues";
            this.dgvOpcValues.RowTemplate.Height = 23;
            this.dgvOpcValues.Size = new System.Drawing.Size(702, 315);
            this.dgvOpcValues.TabIndex = 0;
            // 
            // chName
            // 
            this.chName.DataPropertyName = "Name";
            this.chName.HeaderText = "Name";
            this.chName.Name = "chName";
            this.chName.ReadOnly = true;
            this.chName.Width = 400;
            // 
            // chValue
            // 
            this.chValue.DataPropertyName = "Value";
            this.chValue.HeaderText = "Value";
            this.chValue.Name = "chValue";
            // 
            // frmOpcValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(702, 315);
            this.Controls.Add(this.dgvOpcValues);
            this.Name = "frmOpcValues";
            this.Text = "frmOpcValues";
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpcValues)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOpcValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn chName;
        private System.Windows.Forms.DataGridViewTextBoxColumn chValue;
    }
}