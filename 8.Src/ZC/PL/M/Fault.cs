using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PLC;

namespace PL
{
    public class Fault
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Fault(string address)
        {

        }

        public ItemDefine ItemDefine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsFault
        {
            get
            {
                return FaultUI.Checked;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public CheckBox FaultUI
        {
            get;
            set;
        }
    }
}
