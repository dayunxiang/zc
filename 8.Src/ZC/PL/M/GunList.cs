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

    public class WorkGunGroup
    {
        /// <summary>
        /// 
        /// </summary>
        public WorkGunGroup()
        {
            this.WorkGuns = new GunList();
            this.SearchGuns = new GunList();
        }

        /// <summary>
        /// 
        /// </summary>
        public GunList WorkGuns
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public GunList SearchGuns
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun First()
        {
            if (this.SearchGuns.Count > 0)
                return this.SearchGuns.First();
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun Last()
        {
            if (this.SearchGuns.Count > 0)
                return this.SearchGuns.Last();
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tailGun"></param>
        /// <returns></returns>
        internal bool IsIncludeGun(Gun tailGun)
        {
            return this.SearchGuns.IsIncludeGun(tailGun);
        }
    }
}
