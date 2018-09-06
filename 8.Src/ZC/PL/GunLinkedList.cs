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
        internal WorkGunGroup GetFirstGuns(int count)
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("gun linked list count == 0");
            }

            WorkGunGroup wgg = new WorkGunGroup();

            //GunList r = new GunList();

            var currentGun = this.First.Value;
            while (count > 0)
            {
                Debug.Assert(currentGun != null);

                if (currentGun.CanUse())
                {
                    wgg.WorkGuns.Add(currentGun);
                    count--;
                }

                wgg.SearchGuns.Add(currentGun);
                currentGun = currentGun.Next;
                if (currentGun == null)
                {
                    break;
                }
            }
            return wgg;
        }
    }
}
