using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    //public class MaterialHeapDefine {
    //    public string DamAddress { get; set; }
    //    public string MaterialIdAddress { get; set; }
    //    public string BeginLocationAddress { get; set; }
    //    public string EndLocationAddress { get; set; }
    //    public string CanWetAddress { get; set; }

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <returns></returns>
    //    public MaterialHeap Create() {
    //        return new MaterialHeap(this);
    //    }
    //}

    public class MaterialHeapPositionDefine {
        public string StartPositionAddress { get; set; }
        public string EndPositionAddress { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MaterialHeapPosition Create() {
            return new MaterialHeapPosition(this);
        }
    }
}
