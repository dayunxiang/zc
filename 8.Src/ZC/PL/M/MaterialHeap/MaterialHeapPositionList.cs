using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {


    public class MaterialHeapPositionList : List<MaterialHeapPosition> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dams"></param>
        public void ReadFromPlc() {
            foreach (var materialHeapPosition in this) {
                materialHeapPosition.ReadFromPlc();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        internal MaterialHeapPosition FindByGun(Gun gun) {
            //foreach (var materialHeap in this) {
            //    if (materialHeap.Dam == gun.Dam) {
            //        decimal gunBeginLocation = gun.Location - Config.GunRadius;
            //        decimal gunEndLocation = gun.Location + Config.GunRadius;

            //        if (materialHeap.IsInRange(gunBeginLocation) ||
            //                materialHeap.IsInRange(gunEndLocation)) {
            //            return materialHeap;
            //        }
            //    }
            //}

            //return null;

            // todo
            //
            throw new NotImplementedException();
        }
    }
}
