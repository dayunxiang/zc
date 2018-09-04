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
using Xdgk.Common;

namespace ZC
{
    public partial class frmMain : Form
    {

        #region Members
        static private NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private Service _service = new Service();
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                _service.StartLoop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            _service.StopLoop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WindowMessageCode.WM_COPYDATA:
                    var cds = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                    ProcessCopyDataStruct(cds);
                    break;

                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        private void ProcessCopyDataStruct(COPYDATASTRUCT cds)
        {
            var sb = new StringBuilder();
            sb.AppendLine("cbData: " + cds.cbData.ToString());
            sb.AppendLine("dwData: " + cds.dwData.ToString());
            sb.AppendLine("lpData: " + cds.lpData);
            _logger.Debug(sb.ToString());


            var items = JsonConverter.ToObject(cds.lpData);

            _logger.Debug(items.ToString());
            _logger.Debug(items.Count);
            foreach (var item in items)
            {
                _logger.Debug("{0}:{1}", item.Command, item.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetNames_Click(object sender, EventArgs e)
        {
            _service.ItemDefines = CreateItemDefines(txtNames.Text);
            _service.ItemDefines.ValueChanged += ItemDefines_ValueChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemDefines_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            Action a = () =>
            {
                var itemDefines = sender as ItemDefineList;
                txtResult.Text = itemDefines.ToString();
            };

            if (InvokeRequired)
            {
                Invoke(a);
            }
            else
            {
                a();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="names"></param>
        /// <returns></returns>
        private ItemDefineList CreateItemDefines(string names)
        {
            var r = new ItemDefineList();
            var nameArray = names.Split('\n');
            foreach (var name in nameArray)
            {
                var name2 = name.Trim();
                if (name.Length > 0)
                {
                    var itemDefine = new ItemDefine()
                    {
                        ItemName = name2,
                        ItemPath = "",
                    };
                    r.Add(itemDefine);
                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            _service.ItemDefines.SetValue("", "abc", 123);
        }
    }
}
