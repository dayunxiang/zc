using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Xdgk.Common;

namespace PLForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.ThreadException += Application_ThreadException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Application.Run(new frmMain());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ProcessException(e.Exception);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        static private void ProcessException(Exception ex)
        {
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ProcessException(e.ExceptionObject as Exception);
        }
    }
}
