using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xdgk.Common;
using NUnit.UiKit;
using PL;

namespace PLForm
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// 
        /// </summary>
        private bool _isClose = false;

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
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text = AppConfigReader.Read<string>("MainText", "---");
            Lm.Logs.Add(new TxtLog(this.txtLog));

            //App.GetApp().AppController.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClearLogs_Click(object sender, EventArgs e)
        {
            this.txtLog.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            var appController = App.GetApp().AppController;
            if (appController.ControllerStatus.IsWorking())
            {
                var s = "工作中, 无法退出!";
                NUnit.UiKit.UserMessage.DisplayFailure(s);
            }
            else
            {
                var s = "确定退出吗?";
                if (UserMessage.Ask(s) == System.Windows.Forms.DialogResult.Yes)
                {
                    _isClose = true;
                    appController.ControllerStatus.Value = ControllerStatusEnum.NotRun;
                    Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isClose)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }
    }

    public class TxtLog : ILog
    {
        private const int MAX = 10000;
        private int _count = 0;
        private RichTextBox _txt;

        /// <summary>
        /// 
        /// </summary>
        public TxtLog(RichTextBox txt)
        {
            _txt = txt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void D(string s)
        {
            if(_count > MAX)
            {
                _count = 0;
                _txt.Clear();
            }

            var text = string.Format("{0} {1}{2}", DateTime.Now.ToString(), s, Environment.NewLine);
            _txt.AppendText(text);
            _count++;
        }
    }
}