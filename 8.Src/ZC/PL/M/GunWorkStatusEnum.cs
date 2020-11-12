using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public enum GunWorkStatusEnum {
        Normal = 0,
        NotWorkWithMaterialHeap = 1,
        NotWorkWithCart = 2,
        NotWorkWithMark = 3,
        NotWorkWithFault = 4,
        NotWorkWithRemote = 5,
    }
}
