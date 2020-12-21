using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class GunDefine {
        public int No { get; set; }
        public string Name { get; set; }
        public string Switch { get; set; }
        public string Mark { get; set; }
        public string Fault { get; set; }
        public string Remote { get; set; }
        //public string WorkStatus { get; set; }
        public decimal Location { get; set; }
        public string AssociateDamAreaName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Gun Create() {
            return new Gun(this) {
                Fault = new Fault(this.Fault),
                Mark = new Mark(this.Mark),
                Remote = new Remote(this.Remote),
                //GunWorkStatus = new GunWorkStatus(this.WorkStatus),
                Switch = new Switch(this.Switch),
            };
        }

    }
}
