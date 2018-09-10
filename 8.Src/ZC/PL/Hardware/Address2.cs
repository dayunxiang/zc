using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware
{

    public class Address2
    {
        public string AutoManual { get; set; }
        public string ZtPlcStatus { get; set; }
        public string AppControlStatus { get; set; }
        public string CycleCount { get; set; }
        public string PlTimeSecond { get; set; }
        public string CycleMode { get; set; }
        public string WorkDam { get; set; }
        public string GunCountPerGroup { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Address2 Instance
        {
            get
            {
                if (_instance == null)
                {
                    var json = File.ReadAllText("address2.json");
                    _instance = JsonConvert.DeserializeObject<Address2>(json);
                }
                return _instance;
            }
        } static private Address2 _instance = null;

    }
}
