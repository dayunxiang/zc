using System;
using System.IO;
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

namespace PLForm {
    public partial class frmMain : Form {
        /// <summary>
        /// 
        /// </summary>
        private bool _isClose = false;

        /// <summary>
        /// 
        /// </summary>
        public frmMain() {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e) {

            this.Text = AppConfigReader.Read<string>("MainText", "---");
            this.Text += " - " + Application.ProductVersion;

            tssAppStatus.Text = "OPC 未连接";

            PLC.MyLogManager.Logs.Add(new TxtLog(this.txtLog));
            App.GetApp().Opc.ConnectedEvent += Opc_ConnectedEvent;
            App.GetApp().AppController.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Opc_ConnectedEvent(object sender, EventArgs e) {
            this.tssAppStatus.Text = "OPC 已连接, " + DateTime.Now.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbClearLogs_Click(object sender, EventArgs e) {
            this.txtLog.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e) {
            var appController = App.GetApp().AppController;

            if (appController.ControllerStatus.IsWorking()) {
                var s = "工作中, 无法退出!";
                NUnit.UiKit.UserMessage.DisplayFailure(s);
            } else {
                var s = "确定退出吗?";
                if (UserMessage.Ask(s) == System.Windows.Forms.DialogResult.Yes) {
                    _isClose = true;
                    var opc = App.GetApp().Opc;
                    if (opc.IsConnected()) {
                        appController.ControllerStatus.Value = ControllerStatusEnum.NotRun;
                    }
                    Close();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e) {
            if (!_isClose) {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSaveLog_Click(object sender, EventArgs e) {
            try {
                var dt = DateTime.Now;
                string fileName = string.Format(
                    "{0}\\log\\{1}.txt",
                    Application.StartupPath,
                    dt.ToString("yyyy_MM_dd_HH_mm_ss")
                    );
                File.WriteAllText(fileName, this.txtLog.Text);
                NUnit.UiKit.UserMessage.DisplayInfo("日志保存成功");
            } catch (Exception ex) {
                ExceptionLogger.Log(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbGunInfo_Click(object sender, EventArgs e) {
            var frmGunLocation = new frmGunInfo();
            frmGunLocation.Dams = App.GetApp().Dams;
            frmGunLocation.ShowDialog();
        }
    }
}
