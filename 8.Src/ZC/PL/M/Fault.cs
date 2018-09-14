using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PLC;

namespace PL
{
    public class Fault : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Fault(string address)
            : base(address)
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
                if (Config.IsMock)
                {
                    return IsFaultMock();
                }
                else
                {
                    return IsFaultFact();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsFaultMock()
        {
            return FaultUI.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsFaultFact()
        {
            var val = ReadFromOpc();
            var n = Convert.ToInt32(val);
            return n == (int)FaultStatusEnum.Fault;
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
