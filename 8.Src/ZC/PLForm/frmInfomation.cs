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
    public partial class frmInfomation : Form {

        public DamLinkedList Dams { get; set; }
        public MaterialAreaList MaterialAreas { get; set; }
        public CartList Carts { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public frmInfomation() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmGunLocation_Load(object sender, EventArgs e) {
            RefreshListViews();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RefreshListViews() {
            FillGunListView();
            FillMaterialListView();
            FillCartListView();
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillGunListView() {
            this.lvGun.Items.Clear();

            foreach (var dam in this.Dams) {
                foreach (var gun in dam.Guns) {
                    var lvi = CreateGunListViewItem(gun);
                    this.lvGun.Items.Add(lvi);
                }
            }
        }

        private void FillMaterialListView() {
            this.lvMaterial.Items.Clear();

            this.MaterialAreas.ForEach(ma => {
                int n = 0;
                ma.MaterialHeapPositions.ForEach(mhp => {
                    var lvi = CreateMaterialHeapPositionListViewItem(n++, mhp, ma);
                    this.lvMaterial.Items.Add(lvi);
                });
            });
        }

        private void FillCartListView() {
            this.lvCart.Items.Clear();

            this.Carts.ForEach(c => {
                int n = 0;
                var lvi = CreateCartListViewItem(c);
                this.lvCart.Items.Add(lvi);
            });
        }

        private ListViewItem CreateCartListViewItem(Cart c) {
            var items = new string[] { 
                c.Name, 
                c.ReadLocation ().ToString ()
            };
            var r = new ListViewItem(items);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mhp"></param>
        /// <param name="ma"></param>
        /// <returns></returns>
        private ListViewItem CreateMaterialHeapPositionListViewItem(int n, MaterialHeapPosition mhp, MaterialArea ma) {
            var items = new string[] { 
                ma.ReadStockGroupIdString() + '-' +n,
                string.Format ("[{0}, {1}]",mhp.ReadStartPosition(), mhp.ReadEndPosition ()),
                mhp.ReadAttribute().ToString ()
            };
            var r = new ListViewItem(items);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        private ListViewItem CreateGunListViewItem(Gun gun) {
            var items = new string[] { gun.Name, gun.Location.ToString(), gun.AssociateDamArea.Name };
            var r = new ListViewItem(items);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRefresh_Click(object sender, EventArgs e) {
            RefreshListViews();
        }
    }
}
