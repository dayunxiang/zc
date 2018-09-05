using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Switch
    {
        //private string p;

        public Switch(string address)
        {
            // TODO: Complete member initialization
            //this.p = p;
        }
        public ItemDefine ItemDefine { get; set; }
        public bool IsOpened
        {
            get {
                throw new NotImplementedException();
            }
        }

        public void Open()
        {

        }
        public void Close()
        {

        }
    }
}
