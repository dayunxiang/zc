using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {
    public enum CanWetEnum {
        No = 0,
        Yes = 1,
    }

    public class MaterialHeap {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        public MaterialHeap(MaterialHeapDefine define) {
            this.Define = define;
        }

        public MaterialHeapDefine Define { get; private set; }

        public bool IsReadedFromPlc { get; private set; }
        public DateTime ReadDateTime { get; private set; }

        public Dam Dam { get; set; }
        public int DamValue { get; set; }
        public int MaterialId { get; set; }
        public decimal BeginLocation { get; set; }
        public decimal EndLocation { get; set; }
        public bool CanWet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsInRange(decimal location) {
            return
                location > this.BeginLocation &&
                location < this.EndLocation;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadFromPlc() {
            var itemNames = new string[]{
                this.Define.DamAddress,
                this.Define.MaterialIdAddress,
                this.Define.BeginLocationAddress,
                this.Define.EndLocationAddress,
                this.Define.CanWetAddress,
            };

            var values = App.GetApp().Opc.Read(itemNames);
            this.DamValue = Convert.ToInt32(values[0]);
            //this.Dam = dams.FindDamByValue(this.DamValue);
            //this.Dam.MaterialHeaps.Add(this);

            this.MaterialId = Convert.ToInt32(values[1]);
            this.BeginLocation = Convert.ToDecimal(values[2]);
            this.EndLocation = Convert.ToDecimal(values[3]);
            this.CanWet = Convert.ToInt32(values[4]) == (int)CanWetEnum.Yes;

            this.IsReadedFromPlc = true;
            this.ReadDateTime = DateTime.Now;
        }
    }

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
