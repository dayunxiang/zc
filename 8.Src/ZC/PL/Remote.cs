using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Remote
    {
        //private string p;

        public Remote(string address)
        {
            // TODO: Complete member initialization
            //this.p = p;
        }
        public ItemDefine ItemDefine { get; set; }

        public bool IsRemote
        {
            get
            {
                return RemoteUI.Checked;
            }
        }

        public CheckBox RemoteUI
        {
            get;
            set;
        }
    }
}
