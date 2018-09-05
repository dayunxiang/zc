using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Remote
    {
        //private string p;

        public Remote(string address)
        {
            // TODO: Complete member initialization
            //this.p = p;
        }
        public ItemDefine ItemDefine { get; set; }
        public bool IsRemote
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
