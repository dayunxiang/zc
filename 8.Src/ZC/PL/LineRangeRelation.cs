using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PL {

    public enum LineRangeRelation {
        Disconnection,
        CrossAtBegin,
        CrossAtEnd,
        Include,
        BeIncluded,
    }
}
