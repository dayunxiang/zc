using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class SavingFrameEventArgs : EventArgs {
        public Frame Frame { get; set; }
    }
}
