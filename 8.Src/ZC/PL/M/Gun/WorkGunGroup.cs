using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {
    public class WorkGunGroup {
        /// <summary>
        /// 
        /// </summary>
        public WorkGunGroup() {
            this.WorkGuns = new GunList();
            this.SearchedGuns = new GunList();
        }

        /// <summary>
        /// 
        /// </summary>
        public GunList WorkGuns {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public GunList SearchedGuns {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun GetFirstGun() {
            if (this.SearchedGuns.Count > 0)
                return this.SearchedGuns.First();
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Gun GetLastGun() {
            if (this.SearchedGuns.Count > 0)
                return this.SearchedGuns.Last();
            else
                return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tailGun"></param>
        /// <returns></returns>
        internal bool IsIncludeGun(Gun tailGun) {
            return this.SearchedGuns.IsIncludeGun(tailGun);
        }

        /// <summary>
        /// get first gun.dam value
        /// </summary>
        public int GetWorkingDamValue() {
            if (this.WorkGuns.Count == 0) {
                throw new InvalidOperationException("work guns count == 0");
            }

            var gun = this.WorkGuns.GetFirst();
            return gun.Dam.GetDamValue();
        }
    }
}
