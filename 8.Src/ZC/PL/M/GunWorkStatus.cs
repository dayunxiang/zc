using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class GunWorkStatus : PlcAddress {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public GunWorkStatus(string address)
            : base(address) {
            }


        /// <summary>
        /// 
        /// </summary>
        public GunWorkStatusEnum Status {
            get {
                var obj = ReadFromOpc();
                var n = Convert.ToInt32(obj);
                return (GunWorkStatusEnum)n;
            }
            set {
                WriteToOpc(value);
            }
        }

    }
}
