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
        /// <param name="gun"></param>
        /// <returns></returns>
        internal MaterialHeapPositionList FindByGun(Gun gun) {
            MaterialHeapPositionList r = new MaterialHeapPositionList();

            decimal gunBeginLocation = gun.Location - Config.GunRadius;
            gunBeginLocation =  Math.Max(gunBeginLocation, 0m);

            decimal gunEndLocation = gun.Location + Config.GunRadius;

            foreach (var materialHeapPosition in this) {
                if (materialHeapPosition.IsInRange(gunBeginLocation) ||
                    materialHeapPosition.IsInRange(gunEndLocation)) {

                    r.Add(materialHeapPosition);

                }
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanWet() {
            foreach (var mhp in this) {
                if (mhp.CanNotWet()) {
                    return false;
                }
            }

            return true;
        }
    }
}
