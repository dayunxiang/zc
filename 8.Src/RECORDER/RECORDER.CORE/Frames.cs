using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class Frames : List<Frame> {
        private const int DEFAULT_CAPACITY = 5000;

        /// <summary>
        /// 
        /// </summary>
        public Frames()
            : base(DEFAULT_CAPACITY) {

        }

    }
}
