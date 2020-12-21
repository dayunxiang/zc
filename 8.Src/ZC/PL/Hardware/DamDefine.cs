using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class DamDefine {
        /// <summary>
        /// 
        /// </summary>
        public DamDefine() {
            this.GunDefines = new List<GunDefine>();
        }

        public string Name { get; set; }
        public int No { get; set; }
        public List<DamAreaDefine> DamAreaDefines { get; set; }
        public List<GunDefine> GunDefines { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dam Create(CartList carts) {
            var gunLinkedList = CreateGunLinkedList();

            var dam = new Dam() {
                No = this.No,
                Name = this.Name,
                Guns = gunLinkedList,
            };

            foreach (var damAreaDefine in this.DamAreaDefines) {
                var damArea = damAreaDefine.Create();
                dam.DamAreas.Add(damArea);
            }

            foreach (var gun in gunLinkedList) {
                gun.Dam = dam;

                var damArea = dam.DamAreas.GetByName(gun.Define.AssociateDamAreaName);
                gun.AssociateDamArea = damArea;
                damArea.Guns.Add(gun);
            }

            return dam;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damAreaDefine"></param>
        /// <param name="gunLinkedList"></param>
        /// <returns></returns>
        private DamArea CreateDamArea(DamAreaDefine damAreaDefine, GunLinkedList gunLinkedList) {
            var damArea = damAreaDefine.Create();
            foreach (var gun in gunLinkedList) {
                //if(gun)
            }

            return damArea;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GunLinkedList CreateGunLinkedList() {
            GunLinkedList guns = new GunLinkedList();

            LinkedListNode<Gun> lastNode = null;
            foreach (var gunDefine in GunDefines) {
                Gun gun = gunDefine.Create();
                if (lastNode == null) {
                    lastNode = guns.AddFirst(gun);
                } else {
                    lastNode = guns.AddAfter(lastNode, gun);
                }
                gun.GunNode = lastNode;
            }
            return guns;
        }
    }
}
