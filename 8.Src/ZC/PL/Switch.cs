using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PLC;

namespace PL
{
    public class Switch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Switch(string address)
        {
            // TODO: Complete member initialization
            //this.p = p;
        }

        /// <summary>
        /// 
        /// </summary>
        public ItemDefine ItemDefine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpened
        {
            get
            {
                // todo: test
                return SwitchUI.BackColor == Color.Green;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            // todo: test
            this.SwitchUI.BackColor = Color.Green;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            // todo: test
            this.SwitchUI.BackColor = SystemColors.Control;
        }

        /// <summary>
        /// 
        /// </summary>
        public Label SwitchUI
        {
            get;
            set;
        }
    }
}
