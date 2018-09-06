using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PL;
namespace PLForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            var dams = App.GetApp().Dams;
            this.ucH1.CreateUI(dams);

            var ac = App.GetApp().AppController;
            ac.AutoManualStatus.AutoManualUI = chkAutoManual;
            ac.ZtPlcStatus.ZtPlcStatusUI = txtZtPlcStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            var dam = App.GetApp().Dams.First.Value;
            var gun0 = dam.Guns.First.Value;
            if(gun0.Switch.IsOpened )
            {
                gun0.Switch.Close();
            }
            else
            {
                gun0.Switch.Open();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            App.GetApp().AppController.Start();
        }
    }
}
