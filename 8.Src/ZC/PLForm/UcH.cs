using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PL;

namespace PLForm
{
    public partial class UcH : UserControl
    {
        private DamLinkedList _dams;
        /// <summary>
        /// 
        /// </summary>
        public UcH()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dams"></param>
        public void CreateUI(DamLinkedList dams)
        {
            _dams = dams;

            int x = 5;
            int y = 5;
            int rowHeight = 25;
            int rowSpace = 3;

            int width = 100;
            int chkWidth = 60;
            int height = 20;

            int n = 0;
            foreach (var dam in dams)
            {
                x = 5 + n * 300;
                y = 5;
                var lblDam = new Label();
                lblDam.Text = string.Format("{0} - {1}", dam.Name, dam.No);
                lblDam.Location = new Point(x, y);
                lblDam.Size = new Size(width * 2, height);
                lblDam.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

                this.Controls.Add(lblDam);
                y += rowHeight + rowSpace;

                foreach (Gun gun in dam.Guns)
                {
                    x = 5 + n * 300;
                    var lblGun = new Label()
                    {
                        Text = string.Format("{0}-{1}-{2}", gun.Dam.Name, gun.Name, gun.No),
                        Location = new Point(x, y),
                        Size = new Size(width, height),
                        BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                    };
                    this.Controls.Add(lblGun);
                    //Console.WriteLine("lblGun: {0}, {1}", lblGun.Text, lblGun.Location);
                    x += lblGun.Width;

                    var chkFault = new CheckBox()
                    {
                        Text = "Fault",
                        Location = new Point(x, y),
                        Size = new Size(chkWidth, height),
                    };
                    this.Controls.Add(chkFault);
                    x += chkWidth;

                    var chkMark = new CheckBox()
                    {
                        Text = "Mark",
                        Location = new Point(x, y),
                        Size = new Size(chkWidth, height),
                    };
                    this.Controls.Add(chkMark);
                    x += chkWidth;

                    var chkRemote = new CheckBox()
                    {
                        Text = "Remote",
                        Location = new Point(x, y),
                        Size = new Size(chkWidth, height),
                        Checked = true,
                    };
                    this.Controls.Add(chkRemote);

                    y += height + 5;
                    //x = 5;

                    //gun.Switch.SwitchUI = lblGun;
                    //gun.Remote.RemoteUI = chkRemote;
                    //gun.Fault.FaultUI = chkFault;
                    //gun.Mark.MarkUI = chkMark;
                }
                n++;
            }
        }
    }
}
