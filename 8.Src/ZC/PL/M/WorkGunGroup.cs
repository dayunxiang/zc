using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{
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
        public Gun GetFirstGun()
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
        public Gun GetLastGun()
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
