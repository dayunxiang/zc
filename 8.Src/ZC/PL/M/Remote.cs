using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{
    public class Remote : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Remote(string address)
            :base(address)
        {
        }

        public ItemDefine ItemDefine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsRemote
        {
            get
            {
                if(Config.IsMock)
                {
                    return IsRemoteMock();
                }
                else
                {
                    return IsRemoteFact();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsRemoteMock()
        {
            return RemoteUI.Checked;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsRemoteFact()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public CheckBox RemoteUI
        {
            get;
            set;
        }
    }
}
