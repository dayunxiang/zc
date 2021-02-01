using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RECORDER.CORE {

    public class RecordInfo {
        public string Name { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int FramesCount { get; set; }

        public int Size { get; set; }
    }
}
