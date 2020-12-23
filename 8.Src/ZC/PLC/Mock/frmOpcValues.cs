using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC {
    public partial class frmOpcValues : Form {

        static public frmOpcValues Instance = new frmOpcValues();

        public frmOpcValues() {
            InitializeComponent();
            FormClosing += frmOpcValues_FormClosing;
        }

        private void frmOpcValues_FormClosing(object sender, FormClosingEventArgs e) {
            e.Cancel = true;
        }

        public Items Items {
            get;
            set;
        }

        public void BindItemsToDgv() {
            this.dgvOpcValues.DataSource = this.Items;
        }

        public object Read(string name) {
            foreach (DataGridViewRow row in this.dgvOpcValues.Rows) {
                if (((string)row.Cells[0].Value).Equals(name)) {
                    var cell = row.Cells[1];
                    //cell.Style.BackColor = Color.Green;
                    new CellColorTimer(cell, Color.Green);
                    return row.Cells[1].Value;
                }
            }

            throw new Exception("not find name: " + name);
        }

        internal void Write(string name, object value) {
            foreach (DataGridViewRow row in this.dgvOpcValues.Rows) {
                if (((string)row.Cells[0].Value).Equals(name)) {
                    var cell = row.Cells[1];
                    cell.Value = (int)value;
                    //cell.Style.BackColor = Color.Green;
                    new CellColorTimer(cell, Color.Red);
                }
            }
        }
    }

}
