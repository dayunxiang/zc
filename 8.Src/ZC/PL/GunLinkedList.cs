using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class GunLinkedList : LinkedList<Gun>
    {
        /// <summary>
        /// 
        /// </summary>
        public GunLinkedList()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        internal GunList GetFirstGuns(int count)
        {
            if(this.Count == 0)
            {
                throw new InvalidOperationException("gun linked list count == 0");
            }

            GunList r = new GunList();

            var current = this.First.Value;
            while (count > 0)
            {
                Debug.Assert(current != null);

                if(!current.Fault.IsFault)
                {
                    r.Add(current);
                    count--;
                }
                current = current.Next;
                if(current == null)
                {
                    break;
                }
            }
            return r;
        }
    }
}
