using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class Frame {

        public Frame() {
            this.NameValuePairs = new NameValuePairs();
        }
        public NameValuePairs NameValuePairs { get; set; }
    }
}
