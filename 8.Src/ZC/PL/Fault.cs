using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{
    public class Fault
    {
        public Fault(string address)
        {

        }

        public ItemDefine ItemDefine { get; set; }

        public bool IsFault
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
