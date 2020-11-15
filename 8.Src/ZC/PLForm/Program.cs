using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Management;
using System.Security.Cryptography;
using Xdgk.Common;

namespace PLForm {

    static class Program {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main() {

            PLC.OpcServerManager.Instance.IsMock = PL.Config.IsMock;

            var encode = Encode(GetBoardID());
            var key = ReadKey();

            //if (!key.Equals(encode)) {
            //    NUnit.UiKit.UserMessage.DisplayFailure(S.InvalidSn);
            //    return;
            //}

            if (Xdgk.Common.Diagnostics.HasPreInstance()) {
                NUnit.UiKit.UserMessage.DisplayInfo(S.ProgramRunning);
                return;
            }

            //AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            //Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new frmMain());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e) {
            ProcessException(e.Exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            ProcessException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        static private void ProcessException(Exception ex) {
            ExceptionLogger.Log(ex);
            ExceptionHandler.Handle(ex);
            //set not run
            //
            PL.App.GetApp().AppController.ControllerStatus.Value = PL.ControllerStatusEnum.NotRun;
            Application.Exit();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public string GetBoardID() {
            string st = "unknown";
            try {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(
                    "Select * from Win32_BaseBoard");
                foreach (ManagementObject mo in mos.Get()) {
                    st = mo["SerialNumber"].ToString();
                }
            } catch (Exception) {
            }
            return st;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public string Encode(string source) {
            const string s = "__";
            var s2 = string.Format("{0}{1}{2}", s, source, s);
            var bs = ASCIIEncoding.ASCII.GetBytes(s2);

            var md5 = MD5.Create();
            var bs2 = md5.ComputeHash(bs);
            return Convert.ToBase64String(bs2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static private string ReadKey() {
            const string keyFile = "plkey.txt";

            string s = string.Empty;
            try {
                s = File.ReadAllText(keyFile);
                s = s.Trim();
            } catch (Exception) {
            }
            return s;
        }
    }
}
