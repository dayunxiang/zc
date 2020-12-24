namespace PLForm {
    partial class frmInfomation {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInfomation));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpGuns = new System.Windows.Forms.TabPage();
            this.tpMaterials = new System.Windows.Forms.TabPage();
            this.tpCarts = new System.Windows.Forms.TabPage();
            this.lvGun = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLocation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAssociateMaterialArea = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvMaterial = new System.Windows.Forms.ListView();
            this.chMaterialName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaterialPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chMaterialAttribute = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCart = new System.Windows.Forms.ListView();
            this.chCartName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCartPosition = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabControl1.SuspendLayout();
            this.tpGuns.SuspendLayout();
            this.tpMaterials.SuspendLayout();
            this.tpCarts.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpGuns);
            this.tabControl1.Controls.Add(this.tpMaterials);
            this.tabControl1.Controls.Add(this.tpCarts);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 436);
            this.tabControl1.TabIndex = 1;
            // 
            // tpGuns
            // 
            this.tpGuns.Controls.Add(this.lvGun);
            this.tpGuns.Location = new System.Drawing.Point(4, 22);
            this.tpGuns.Name = "tpGuns";
            this.tpGuns.Padding = new System.Windows.Forms.Padding(3);
            this.tpGuns.Size = new System.Drawing.Size(376, 410);
            this.tpGuns.TabIndex = 0;
            this.tpGuns.Text = "喷枪";
            this.tpGuns.UseVisualStyleBackColor = true;
            // 
            // tpMaterials
            // 
            this.tpMaterials.Controls.Add(this.lvMaterial);
            this.tpMaterials.Location = new System.Drawing.Point(4, 22);
            this.tpMaterials.Name = "tpMaterials";
            this.tpMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tpMaterials.Size = new System.Drawing.Size(376, 410);
            this.tpMaterials.TabIndex = 1;
            this.tpMaterials.Text = "料堆";
            this.tpMaterials.UseVisualStyleBackColor = true;
            // 
            // tpCarts
            // 
            this.tpCarts.Controls.Add(this.lvCart);
            this.tpCarts.Location = new System.Drawing.Point(4, 22);
            this.tpCarts.Name = "tpCarts";
            this.tpCarts.Padding = new System.Windows.Forms.Padding(3);
            this.tpCarts.Size = new System.Drawing.Size(376, 410);
            this.tpCarts.TabIndex = 2;
            this.tpCarts.Text = "大机";
            this.tpCarts.UseVisualStyleBackColor = true;
            // 
            // lvGun
            // 
            this.lvGun.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chName,
            this.chLocation,
            this.chAssociateMaterialArea});
            this.lvGun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvGun.FullRowSelect = true;
            this.lvGun.GridLines = true;
            this.lvGun.Location = new System.Drawing.Point(3, 3);
            this.lvGun.Name = "lvGun";
            this.lvGun.Size = new System.Drawing.Size(370, 404);
            this.lvGun.TabIndex = 1;
            this.lvGun.UseCompatibleStateImageBehavior = false;
            this.lvGun.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "名称";
            this.chName.Width = 200;
            // 
            // chLocation
            // 
            this.chLocation.Text = "位置";
            this.chLocation.Width = 50;
            // 
            // chAssociateMaterialArea
            // 
            this.chAssociateMaterialArea.Text = "料区";
            this.chAssociateMaterialArea.Width = 100;
            // 
            // lvMaterial
            // 
            this.lvMaterial.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chMaterialName,
            this.chMaterialPosition,
            this.chMaterialAttribute});
            this.lvMaterial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvMaterial.FullRowSelect = true;
            this.lvMaterial.GridLines = true;
            this.lvMaterial.Location = new System.Drawing.Point(3, 3);
            this.lvMaterial.Name = "lvMaterial";
            this.lvMaterial.Size = new System.Drawing.Size(370, 404);
            this.lvMaterial.TabIndex = 2;
            this.lvMaterial.UseCompatibleStateImageBehavior = false;
            this.lvMaterial.View = System.Windows.Forms.View.Details;
            // 
            // chMaterialName
            // 
            this.chMaterialName.Text = "名称";
            this.chMaterialName.Width = 100;
            // 
            // chMaterialPosition
            // 
            this.chMaterialPosition.Text = "起止";
            this.chMaterialPosition.Width = 100;
            // 
            // chMaterialAttribute
            // 
            this.chMaterialAttribute.Text = "属性";
            this.chMaterialAttribute.Width = 100;
            // 
            // lvCart
            // 
            this.lvCart.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCartName,
            this.chCartPosition});
            this.lvCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCart.FullRowSelect = true;
            this.lvCart.GridLines = true;
            this.lvCart.Location = new System.Drawing.Point(3, 3);
            this.lvCart.Name = "lvCart";
            this.lvCart.Size = new System.Drawing.Size(370, 404);
            this.lvCart.TabIndex = 2;
            this.lvCart.UseCompatibleStateImageBehavior = false;
            this.lvCart.View = System.Windows.Forms.View.Details;
            // 
            // chCartName
            // 
            this.chCartName.Text = "名称";
            this.chCartName.Width = 200;
            // 
            // chCartPosition
            // 
            this.chCartPosition.Text = "位置";
            this.chCartPosition.Width = 50;
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(384, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefresh.Image")));
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(36, 22);
            this.tsbRefresh.Text = "刷新";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // frmGunInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmGunInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "位置信息";
            this.Load += new System.EventHandler(this.frmGunLocation_Load);
            this.tabControl1.ResumeLayout(false);
            this.tpGuns.ResumeLayout(false);
            this.tpMaterials.ResumeLayout(false);
            this.tpCarts.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpGuns;
        private System.Windows.Forms.ListView lvGun;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chLocation;
        private System.Windows.Forms.ColumnHeader chAssociateMaterialArea;
        private System.Windows.Forms.TabPage tpMaterials;
        private System.Windows.Forms.ListView lvMaterial;
        private System.Windows.Forms.ColumnHeader chMaterialName;
        private System.Windows.Forms.ColumnHeader chMaterialPosition;
        private System.Windows.Forms.ColumnHeader chMaterialAttribute;
        private System.Windows.Forms.TabPage tpCarts;
        private System.Windows.Forms.ListView lvCart;
        private System.Windows.Forms.ColumnHeader chCartName;
        private System.Windows.Forms.ColumnHeader chCartPosition;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbRefresh;

    }
}