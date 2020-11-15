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

        /// <summary>
        /// 
        /// </summary>
        public bool IsRemote
        {
            get
            {
                    return IsRemoteFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsRemoteFact()
        {
            var val = ReadFromOpc();
            var n = Convert.ToInt32(val);
            return n == (int)RemoteStatusEnum.Remote;
        }

    }
}
