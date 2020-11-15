using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class AutoManualStatus : PlcAddress {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public AutoManualStatus(string address)
            : base(address) {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AutoManualStatusEnum Read() {
                return ReadFact();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AutoManualStatusEnum ReadFact() {
            var val = ReadFromOpc();
            var n = Convert.ToInt32(val);
            return (AutoManualStatusEnum)n;
        }
    }
}
