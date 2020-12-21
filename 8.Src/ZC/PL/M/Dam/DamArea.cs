using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using PL.Hardware;

namespace PL {

    public class DamArea {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        public DamArea(DamAreaDefine define) {
            if (define == null) {
                throw new ArgumentNullException("damAreaDefine");
            }

            this.Define = define;
            this.Guns = new GunList();
        }

        /// <summary>
        /// 
        /// </summary>
        public DamAreaDefine Define { get; private set; }

        public string Name { get { return this.Define.Name; } }

        public GunList Guns {
            get;
            private set;
        }
    }
}
