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
        public Record() {
            this.Frames = new Frames();

        }

        /// <summary>
        /// 
        /// </summary>
        public Frames Frames {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Frame GetFirstFrame() {
            return this.GetFrame(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Record FromJsonFile(string jsonFilePath) {
            var json = File.ReadAllText(jsonFilePath);
            var record = JsonConvert.DeserializeObject<Record>(json);

            // set frame index
            //
            for (int i = 0; i < record.Frames.Count; i++) {
                record.Frames[i].FrameIndex = i;    
            }

            return record;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        internal Frame GetFrame(int index) {
            if (index < this.Frames.Count)
                return this.Frames[index];
            else
                return null;
        }
    }
}
