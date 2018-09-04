using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NUnit.UiKit;
using Xdgk.Common;
using NLog;
using Newtonsoft.Json;
using ZC.Argument;

namespace ZC
{
    static class Program
    {
        static private NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            IntPtr handle, hwnd;

            if (Xdgk.Common.Diagnostics.HasPreInstance(out handle, out hwnd))
            {
                _logger.Debug("handle: {0}, hwnd: {1}", handle, hwnd);

                Win32API.SetForegroundWindow((int)hwnd);

                var s = JsonConverter.ToString(Create());
                var bs = System.Text.Encoding.Default.GetBytes(s);
                COPYDATASTRUCT cds = new COPYDATASTRUCT()
                {
                    dwData = (IntPtr)0,
                    cbData = bs.Length + 1,
                    lpData = s,
                };
                Win32API.SendMessage((int)hwnd, WindowMessageCode.WM_COPYDATA, 0, ref cds);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }

        static ItemList Create()
        {
            var r = new ItemList();
            r.Add(new Item() { Command = "ca", Value = "va" });
            r.Add(new Item() { Command = "cb", Value = "vb" });
            r.Add(new Item() { Command = "cc", Value = "vc" });
            r.Add(new Item() { Command = "cd", Value = "vd" });
            return r;
        }
    }
}
