using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RECORDER.CORE {

    public class Record {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDateTime"></param>
        /// <param name="frameTimeSpan"></param>
        public Record(DateTime startDateTime, TimeSpan frameTimeSpan) {
            this.StartDateTime = startDateTime;
            this.FrameTimeSpan = frameTimeSpan;
            this.Frames = new Frames();

        }
        public DateTime StartDateTime { get; set; }
        public TimeSpan FrameTimeSpan { get; set; }

        public Frames Frames { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Record FromJsonFile(string jsonFilePath) {
            var json = File.ReadAllText(jsonFilePath);
            var record = JsonConvert.DeserializeObject<Record>(json);
            return record;
        }
    }
}
