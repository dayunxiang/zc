using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PL;

namespace PLForm {
    public partial class frmGunInfo : Form {

        public DamLinkedList Dams { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public frmGunInfo() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGunLocation_Load(object sender, EventArgs e) {
            FillListView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillListView() {
            foreach (var dam in this.Dams) {
                foreach (var gun in dam.Guns) {
                    var lvi = CreateListViewItem(gun);
                    this.lv.Items.Add(lvi);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        private ListViewItem CreateListViewItem(Gun gun) {
            var items = new string[] { gun.Name, gun.Location.ToString(), gun.AssociateDamArea.Name};
            var r = new ListViewItem(items);
            return r;
        }
    }
}
