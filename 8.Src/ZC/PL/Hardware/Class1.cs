using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware
{
    public class GunDefine
    {
        public int No { get; set; }
        public string Name { get; set; }
        public string Switch { get; set; }
        public string Mark { get; set; }
        public string Fault { get; set; }
        public string Remote { get; set; }

        internal Gun Create()
        {
            return new Gun()
            {
                No = this.No,
                Name = this.Name,
                Fault = new Fault(this.Fault),
                Mark = new Mark(this.Mark),
                Remote = new Remote(this.Remote),
                Switch = new Switch(this.Switch),
            };
        }
    }

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

    public class Serializer
    {
        static public void test()
        {
            var value = new List<DamDefine>();

            var damDefine = new DamDefine()
            {
                No = 1,
                Name = "BD-2"
            };
            var gunDefine1 = new GunDefine()
            {
                No=1,
                Name = "name1",
                Fault = "fault1",
                Mark = "mark1",
                Remote = "remote1",
                Switch = "switch1",
            };

            var gunDefine2 = new GunDefine()
            {
                No = 2,
                Name = "name2",
                Fault = "fault2",
                Mark = "mark2",
                Remote = "remote2",
                Switch = "switch2",
            };
            damDefine.GunDefines.Add(gunDefine1);
            damDefine.GunDefines.Add(gunDefine2);
            value.Add(damDefine);
            var s = JsonConvert.SerializeObject(value);
            Console.WriteLine(s);


            var deserialDamDefines = JsonConvert.DeserializeObject<List<DamDefine>>(s);
            Console.WriteLine(deserialDamDefines.Count);
            Console.WriteLine(deserialDamDefines[0].Name);
        }
    }
}
