using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PLC;

namespace ZC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Service _service = new Service();       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _service.StartLoop();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString ());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _service.StopLoop();
        }
    }
}
