using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware
{

    public class DamDefine
    {
        public DamDefine()
        {
            this.GunDefines = new List<GunDefine>();
        }
        public string Name { get; set; }
        public int No { get; set; }
        public List<GunDefine> GunDefines { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dam Create()
        {
            var guns = CreateGuns();

            var r = new Dam()
            {
                No = this.No,
                   Name = this.Name,
                   Guns = guns,
            };

            foreach (var gun in guns)
            {
                gun.Dam = r;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GunLinkedList CreateGuns()
        {
            GunLinkedList guns = new GunLinkedList();

            LinkedListNode<Gun> lastNode = null;
            foreach (var gunDefine in GunDefines)
            {
                Gun gun = gunDefine.Create();
                if(lastNode == null)
                {
                    lastNode = guns.AddFirst(gun);
                }
                else
                {
                    lastNode = guns.AddAfter(lastNode, gun);
                }
                gun.GunNode = lastNode;
            }
            return guns;
        }
    }
}
