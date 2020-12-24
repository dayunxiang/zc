using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL {
    public class PlException : Exception {
        public PlException(string msg)
            : base(msg) {

        }
    }
}
