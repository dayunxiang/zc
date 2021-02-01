using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using RECORDER.CORE;

namespace RECORDER {
    public partial class frmRecordList : Form {

        static public frmRecordList Instance = new frmRecordList();

        private frmRecordList() {
            InitializeComponent();

            this.listBox1.DoubleClick += listBox1_DoubleClick;
            this.FormClosing += frmRecordList_FormClosing;
            this.listBox1.DisplayMember = "Name";
            this.listBox1.DataSource = RecordInfoLinkedListManager.Instance.RecordInfoLinkedList.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBox1_DoubleClick(object sender, EventArgs e) {
            //var index = this.listBox1.SelectedIndex;
            var item = this.listBox1.SelectedItem;
            if (item != null) {

                using (new CP.Windows.Forms.WaitCursor()) {
                    var recordInfoNode = (RecordInfo)item;

                    var node = RecordInfoLinkedListManager.Instance.RecordInfoLinkedList.Find(recordInfoNode);
                    App.Instance.Player.RecordInfoNode = node;
                    App.Instance.Player.Play();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRecordList_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
            this.Hide();
        }
    }
}
