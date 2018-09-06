using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class ZtPlcStatus
    {
        public ZtPlcStatusEnum Read()
        {
            //todo:
            if(ZtPlcStatusUI.Text.Length==0)
            {
                return ZtPlcStatusEnum.Stop;
            }

            int n = int.Parse(ZtPlcStatusUI.Text);
            return (ZtPlcStatusEnum)n;
        }

        internal void Write(ZtPlcStatusEnum ztPlcStatusEnum)
        {
            //todo:
            this.ZtPlcStatusUI.Text = ((int)ztPlcStatusEnum).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public TextBox ZtPlcStatusUI
        {
            get;
            set;
        }
    }
}
