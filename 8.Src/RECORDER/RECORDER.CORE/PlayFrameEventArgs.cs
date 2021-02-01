using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class PlayFrameEventArgs : EventArgs {
        public PlayFrameEventArgs(Frame frame, int frameIndex) {
            this.Frame = frame;
            this.FrameIndex = frameIndex;
        }
        public Frame Frame { get; private set; }

        public int FrameIndex { get; private set; }
    }
}
