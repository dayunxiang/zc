using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    abstract public class PlcAddress {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public PlcAddress(string address) {
            this.Address = address;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object ReadFromOpc() {
            return App.GetApp().Opc.Read(this.Address);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void WriteToOpc(object value) {
            if (App.GetApp().Opc.IsConnected()) {
                var r = App.GetApp().Opc.Write(this.Address, value);
                if (r.ResultID != Opc.ResultID.S_OK) {
                    var message = string.Format("write opc '{0}' fail, result id is '{1}'",
                        this.Address,
                        r.ResultID);
                    throw new OpcException(message);
                }
            } else {
                throw new InvalidOperationException("opc not connect");
            }
        }
    }
}
