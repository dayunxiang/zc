using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class PlayFrameEventArgs : EventArgs {
        public PlayFrameEventArgs(Frame frame) {
            this.Frame = frame;
        }
        public Frame Frame { get; private set; }
    }
}
