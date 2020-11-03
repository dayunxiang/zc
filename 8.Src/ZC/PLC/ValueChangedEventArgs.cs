using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC {

    public class ValueChangedEventArgs : EventArgs {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemDefine"></param>
        public ValueChangedEventArgs(ItemDefine itemDefine) {
            this.ItemDefine = itemDefine;
        }

        /// <summary>
        /// 
        /// </summary>
        public ItemDefine ItemDefine { get; set; }
    }
}
