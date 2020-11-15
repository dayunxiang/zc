using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class Mark : PlcAddress {
        public Mark(string address)
            : base(address) {
        }

        public ItemDefine ItemDefine { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool IsMarked {
            get {
                    return IsMarkedFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsMarkedFact() {
            var v = ReadFromOpc();
            var n = Convert.ToInt32(v);
            return n == (int)MarkStatusEnum.Marked;
        }
    }

}
