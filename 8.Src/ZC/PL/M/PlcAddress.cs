using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    abstract public class PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public PlcAddress(string address)
        {
            this.Address = address;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address
        {
            get;
            set;
        }
    }
}
