using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class CartDefine {
        public int No { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string FaultAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Cart Create() {
            var c = new Cart(this);
            return c;
        }
    }
}
