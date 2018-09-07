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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ReadFromOpc()
        {
            return App.GetApp().Opc.Read(this.Address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool WriteToOpc(object value)
        {
            var r = App.GetApp().Opc.Write(this.Address, value);
            return r.ResultID == Opc.ResultID.S_OK;
        }
    }
}
