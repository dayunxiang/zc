using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Gun
    {

        public Gun()
        {

        }

        public LinkedListNode<Gun> GunNode { get; set; }
        public Fault Fault { get; set; }
        public Mark Mark { get; set; }
        public Remote Remote { get; set; }
        public Switch Switch { get; set; }
        public Dam Dam { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Gun Next
        {
            get
            {
                var nextNode = this.GunNode.Next;
                if(nextNode != null)
                {
                    return nextNode.Value;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
