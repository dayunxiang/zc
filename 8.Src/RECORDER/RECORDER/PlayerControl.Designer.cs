namespace RECORDER {
    partial class PlayerControl {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerControl));
            this.tbRecord = new System.Windows.Forms.TrackBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPlay = new System.Windows.Forms.ToolStripButton();
            this.tsbPause = new System.Windows.Forms.ToolStripButton();
            this.tsbStop = new System.Windows.Forms.ToolStripButton();
            this.tsbPrevRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbPrevFrame = new System.Windows.Forms.ToolStripButton();
            this.tsbNextFrame = new System.Windows.Forms.ToolStripButton();
            this.tsbNextRecord = new System.Windows.Forms.ToolStripButton();
            this.tsbSpeed = new System.Windows.Forms.ToolStripComboBox();
            this.tsbRecordList = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPosition = new System.Windows.Forms.Label();
            this.lblDateTime = new System.Windows.Forms.Label();
            this.lblPositionValue = new System.Windows.Forms.Label();
            this.lblDateTimeValue = new System.Windows.Forms.Label();
            this.lblRecordFileSize = new System.Windows.Forms.Label();
            this.lblRecordFile = new System.Windows.Forms.Label();
            this.lblRecordFileValue = new System.Windows.Forms.Label();
            this.lblRecordFileSizeValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tbRecord)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbRecord
            // 
            this.tbRecord.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbRecord.Location = new System.Drawing.Point(0, 0);
            this.tbRecord.Name = "tbRecord";
            this.tbRecord.Size = new System.Drawing.Size(391, 45);
            this.tbRecord.TabIndex = 1;
            this.tbRecord.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbRecord.Value = 5;
            this.tbRecord.Scroll += new System.EventHandler(this.tbRecord_Scroll);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPlay,
            this.tsbPause,
            this.tsbStop,
            this.tsbPrevRecord,
            this.tsbPrevFrame,
            this.tsbNextFrame,
            this.tsbNextRecord,
            this.tsbSpeed,
            this.tsbRecordList});
            this.toolStrip1.Location = new System.Drawing.Point(0, 45);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(391, 39);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPlay
            // 
            this.tsbPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPlay.Name = "tsbPlay";
            this.tsbPlay.Size = new System.Drawing.Size(23, 36);
            this.tsbPlay.Text = "播放";
            this.tsbPlay.Click += new System.EventHandler(this.tsbPlay_Click);
            // 
            // tsbPause
            // 
            this.tsbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPause.Image = ((System.Drawing.Image)(resources.GetObject("tsbPause.Image")));
            this.tsbPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPause.Name = "tsbPause";
            this.tsbPause.Size = new System.Drawing.Size(36, 36);
            this.tsbPause.Text = "暂停";
            this.tsbPause.Click += new System.EventHandler(this.tsbPause_Click);
            // 
            // tsbStop
            // 
            this.tsbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbStop.Image")));
            this.tsbStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbStop.Name = "tsbStop";
            this.tsbStop.Size = new System.Drawing.Size(36, 36);
            this.tsbStop.Text = "停止";
            this.tsbStop.Click += new System.EventHandler(this.tsbStop_Click);
            // 
            // tsbPrevRecord
            // 
            this.tsbPrevRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrevRecord.Image")));
            this.tsbPrevRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevRecord.Name = "tsbPrevRecord";
            this.tsbPrevRecord.Size = new System.Drawing.Size(36, 36);
            this.tsbPrevRecord.Text = "上一个录像";
            this.tsbPrevRecord.Click += new System.EventHandler(this.tsbPrevRecord_Click);
            // 
            // tsbPrevFrame
            // 
            this.tsbPrevFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrevFrame.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrevFrame.Image")));
            this.tsbPrevFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevFrame.Name = "tsbPrevFrame";
            this.tsbPrevFrame.Size = new System.Drawing.Size(36, 36);
            this.tsbPrevFrame.Text = "上一帧";
            this.tsbPrevFrame.Click += new System.EventHandler(this.tsbPrevFrame_Click);
            // 
            // tsbNextFrame
            // 
            this.tsbNextFrame.AccessibleDescription = "";
            this.tsbNextFrame.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextFrame.Image = ((System.Drawing.Image)(resources.GetObject("tsbNextFrame.Image")));
            this.tsbNextFrame.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextFrame.Name = "tsbNextFrame";
            this.tsbNextFrame.Size = new System.Drawing.Size(36, 36);
            this.tsbNextFrame.Text = "下一帧";
            this.tsbNextFrame.Click += new System.EventHandler(this.tsbNextFrame_Click);
            // 
            // tsbNextRecord
            // 
            this.tsbNextRecord.AccessibleDescription = "";
            this.tsbNextRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbNextRecord.Image = ((System.Drawing.Image)(resources.GetObject("tsbNextRecord.Image")));
            this.tsbNextRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextRecord.Name = "tsbNextRecord";
            this.tsbNextRecord.Size = new System.Drawing.Size(36, 36);
            this.tsbNextRecord.Text = "下一个录像";
            this.tsbNextRecord.Click += new System.EventHandler(this.tsbNextRecord_Click);
            // 
            // tsbSpeed
            // 
            this.tsbSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsbSpeed.Name = "tsbSpeed";
            this.tsbSpeed.Size = new System.Drawing.Size(75, 39);
            this.tsbSpeed.ToolTipText = "播放速度";
            this.tsbSpeed.SelectedIndexChanged += new System.EventHandler(this.tsbSpeed_SelectedIndexChanged);
            // 
            // tsbRecordList
            // 
            this.tsbRecordList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRecordList.Image = ((System.Drawing.Image)(resources.GetObject("tsbRecordList.Image")));
            this.tsbRecordList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRecordList.Name = "tsbRecordList";
            this.tsbRecordList.Size = new System.Drawing.Size(36, 36);
            this.tsbRecordList.Text = "录像列表";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblPosition, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDateTime, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPositionValue, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblDateTimeValue, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblRecordFileSize, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblRecordFile, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRecordFileValue, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblRecordFileSizeValue, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 84);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(391, 139);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lblPosition
            // 
            this.lblPosition.Location = new System.Drawing.Point(3, 0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(100, 23);
            this.lblPosition.TabIndex = 0;
            this.lblPosition.Text = "Position:";
            this.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTime
            // 
            this.lblDateTime.Location = new System.Drawing.Point(3, 23);
            this.lblDateTime.Name = "lblDateTime";
            this.lblDateTime.Size = new System.Drawing.Size(100, 23);
            this.lblDateTime.TabIndex = 1;
            this.lblDateTime.Text = "时间:";
            this.lblDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPositionValue
            // 
            this.lblPositionValue.Location = new System.Drawing.Point(109, 0);
            this.lblPositionValue.Name = "lblPositionValue";
            this.lblPositionValue.Size = new System.Drawing.Size(250, 23);
            this.lblPositionValue.TabIndex = 2;
            this.lblPositionValue.Text = "#position";
            this.lblPositionValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDateTimeValue
            // 
            this.lblDateTimeValue.Location = new System.Drawing.Point(109, 23);
            this.lblDateTimeValue.Name = "lblDateTimeValue";
            this.lblDateTimeValue.Size = new System.Drawing.Size(250, 23);
            this.lblDateTimeValue.TabIndex = 3;
            this.lblDateTimeValue.Text = "#time";
            this.lblDateTimeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordFileSize
            // 
            this.lblRecordFileSize.Location = new System.Drawing.Point(3, 69);
            this.lblRecordFileSize.Name = "lblRecordFileSize";
            this.lblRecordFileSize.Size = new System.Drawing.Size(100, 23);
            this.lblRecordFileSize.TabIndex = 4;
            this.lblRecordFileSize.Text = "文件大小:";
            this.lblRecordFileSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordFile
            // 
            this.lblRecordFile.Location = new System.Drawing.Point(3, 46);
            this.lblRecordFile.Name = "lblRecordFile";
            this.lblRecordFile.Size = new System.Drawing.Size(100, 23);
            this.lblRecordFile.TabIndex = 5;
            this.lblRecordFile.Text = "文件:";
            this.lblRecordFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordFileValue
            // 
            this.lblRecordFileValue.Location = new System.Drawing.Point(109, 46);
            this.lblRecordFileValue.Name = "lblRecordFileValue";
            this.lblRecordFileValue.Size = new System.Drawing.Size(250, 23);
            this.lblRecordFileValue.TabIndex = 6;
            this.lblRecordFileValue.Text = "#file";
            this.lblRecordFileValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRecordFileSizeValue
            // 
            this.lblRecordFileSizeValue.Location = new System.Drawing.Point(109, 69);
            this.lblRecordFileSizeValue.Name = "lblRecordFileSizeValue";
            this.lblRecordFileSizeValue.Size = new System.Drawing.Size(250, 23);
            this.lblRecordFileSizeValue.TabIndex = 4;
            this.lblRecordFileSizeValue.Text = "#size";
            this.lblRecordFileSizeValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbRecord);
            this.Name = "PlayerControl";
            this.Size = new System.Drawing.Size(391, 223);
            ((System.ComponentModel.ISupportInitialize)(this.tbRecord)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar tbRecord;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPlay;
        private System.Windows.Forms.ToolStripButton tsbPause;
        private System.Windows.Forms.ToolStripButton tsbStop;
        private System.Windows.Forms.ToolStripButton tsbPrevRecord;
        private System.Windows.Forms.ToolStripButton tsbPrevFrame;
        private System.Windows.Forms.ToolStripButton tsbNextFrame;
        private System.Windows.Forms.ToolStripButton tsbNextRecord;
        private System.Windows.Forms.ToolStripComboBox tsbSpeed;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label lblDateTime;
        private System.Windows.Forms.Label lblPositionValue;
        private System.Windows.Forms.Label lblDateTimeValue;
        private System.Windows.Forms.Label lblRecordFileSize;
        private System.Windows.Forms.ToolStripButton tsbRecordList;
        private System.Windows.Forms.Label lblRecordFile;
        private System.Windows.Forms.Label lblRecordFileValue;
        private System.Windows.Forms.Label lblRecordFileSizeValue;
    }
}
