using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class GunList : List<Gun>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun GetFirst()
        {
            if (this.Count > 0)
                return this[0];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun GetLast()
        {
            if (this.Count > 0)
                return this[this.Count - 1];
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsIncludeGun(Gun gun)
        {
            foreach(var g in this)
            {
                if(g == gun)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
