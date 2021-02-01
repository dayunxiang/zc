using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RECORDER.CORE;

namespace RECORDER {
    public partial class frmDatas : Form {
        //private Record _record;
        private DataGridView _dgv;
        private bool _isFrameSetted = false;

        public frmDatas() {
            InitializeComponent();
            _dgv = this.dataGridView1;
            this.FormClosing += (s, e) => e.Cancel = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDatas_Load(object sender, EventArgs e) {

        }

        //public void SetRecord(Record record) {
        //    Debug.Assert(record != null);

        //    _record = record;
        //    this.lblFrame.Text = _record.Frames.Count.ToString();
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="frameIndex"></param>
        public void SetFrame(Frame frame, int frameIndex) {
            Debug.Assert(frame != null);

            var nvpIndex = 0;
            if (_isFrameSetted) {
                frame.NameValuePairs.ForEach(nvp => {
                    this._dgv.Rows[nvpIndex].Cells[2].Value = nvp.Value;
                    nvpIndex++;
                });
            } else {
                frame.NameValuePairs.ForEach(
                    nvp => {
                        this._dgv.Rows.Add(new object[] { nvpIndex, nvp.Name, nvp.Value });
                        nvpIndex++;
                    }
                    );
                _isFrameSetted = true;
            }

            //this.lblFrame.Text = string.Format("{0} / {1}", frameIndex, _record.Frames.Count);
            //this.lblDateTime.Text =
            //    (_record.StartDateTime +
            //    TimeSpan.FromTicks(
            //        _record.FrameTimeSpan.Ticks * frameIndex))
            //    .ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
    }
}
