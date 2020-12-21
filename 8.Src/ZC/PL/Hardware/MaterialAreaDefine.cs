using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class MaterialAreaDefine {
        public string StockGroupIdAddress { get; set; }
        public string StockGroupIdStringAddress { get; set; }
        public string MaterialIdAddress { get; set; }
        public string MaterialAttributeAddress { get; set; }
        public MaterialHeapPositionDefine[] MaterialHeapPositionDefines { get; set; }

        public MaterialArea Create() {
            var materialArea = new MaterialArea(this);
            foreach (var materialHeapPositionDefine in this.MaterialHeapPositionDefines) {
                var materialHeapPosition = materialHeapPositionDefine.Create();
                materialArea.MaterialHeapPositions.Add(materialHeapPosition);
            }
            return materialArea;
        }
    }
}
