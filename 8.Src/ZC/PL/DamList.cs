using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    //public class DamList : List<Dam>
    public class DamLinkedList : LinkedList<Dam>
    {
        /// <summary>
        /// 
        /// </summary>
        public DamLinkedList()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public GunList GetFirstGuns(PlOptions options)
        {
            Dam firstDam = this.First.Value;
            var workDam = GetWorkDam(firstDam, options);
            return workDam.GetFirstGuns(options.CycleTimes);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dam"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private Dam GetWorkDam(Dam dam, PlOptions options)
        {
            if(options.IsWorkDam(dam))
            {
                return dam;
            }
            else
            {
                var nextDam = dam.GetNextDam();
                return GetWorkDam(nextDam, options);
            }
        }

    }
}
