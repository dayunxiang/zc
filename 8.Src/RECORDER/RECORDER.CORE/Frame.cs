using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RECORDER.CORE {

    public class Frame {




        public Frame() {
            this.DateTime = DateTime.Now;
            this.NameValuePairs = new NameValuePairs();
        }


        /// <summary>
        /// 
        /// </summary>
        public DateTime DateTime {
            get;
            set;
        }

        [JsonIgnore]
        public int FrameIndex {
            get;
            set;
        }

        public NameValuePairs NameValuePairs { get; set; }
    }
}
