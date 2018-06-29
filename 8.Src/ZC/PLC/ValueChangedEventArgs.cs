using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC
{

    public class ValueChangedEventArgs : EventArgs
    {
        public ValueChangedEventArgs(ItemDefine itemDefine)
        {
            this.ItemDefine = itemDefine;
        }
        public ItemDefine ItemDefine { get; set; }
    }
}
