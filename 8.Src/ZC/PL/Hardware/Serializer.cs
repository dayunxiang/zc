using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware
{

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


            var a2Json = JsonConvert.SerializeObject(new Gc());
            Console.WriteLine(a2Json);

        }
    }
}
