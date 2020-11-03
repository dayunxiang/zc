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

    /// <summary>
    /// 
    /// </summary>
    public class TxtLog : PLC.ILog {
        private const int MAX = 5000;

        private int _count = 0;
        private RichTextBox _richTextBox;

        /// <summary>
        /// 
        /// </summary>
        public TxtLog(RichTextBox richTextBox) {
            _richTextBox = richTextBox;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        public void Output(string s) {
            if (_count > MAX) {
                _count = 0;
                _richTextBox.Clear();
            }

            var text = string.Format("{0} {1}{2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"),
                s,
                Environment.NewLine);
            _richTextBox.AppendText(text);
            _count++;
        }
    }
}
