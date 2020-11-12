using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {

    public class MaterialHeapList : List<MaterialHeap> {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dams"></param>
        public void ReadFromPlc() {
            foreach (var materialHeap in this) {
                materialHeap.ReadFromPlc();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        internal MaterialHeap FindByGun(Gun gun) {
            foreach (var materialHeap in this) {
                if (materialHeap.Dam == gun.Dam) {
                    decimal gunBeginLocation = gun.Location - Config.GunRadius;
                    decimal gunEndLocation = gun.Location + Config.GunRadius;

                    if (materialHeap.IsInRange(gunBeginLocation) ||
                            materialHeap.IsInRange(gunEndLocation)) {
                        return materialHeap;
                    }
                }
            }

            return null;
        }
    }
}
