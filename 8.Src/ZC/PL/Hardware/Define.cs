using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware
{

    public class Define {
        public List<CartDefine> CartDefines { get; set; }
        public List<DamDefine> DamDefines { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToJson() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
