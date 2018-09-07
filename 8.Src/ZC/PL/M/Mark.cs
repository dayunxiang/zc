using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Mark : PlcAddress
    {
        public Mark(string address)
            : base(address)
        {
        }

        public ItemDefine ItemDefine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsMarked
        {
            get
            {
                if (Config.IsMock)
                {
                    return IsMarkedMock();
                }
                else
                {
                    return IsMarkedFact();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMarkedMock()
        {
            return MarkUI.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMarkedFact()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public CheckBox MarkUI
        {
            get;
            set;
        }


    }
}
