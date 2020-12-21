using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class DamAreaDefine {
        public int No { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DamArea Create() {
            var damArea = new DamArea(this);
            return damArea;
        }
    }
}
