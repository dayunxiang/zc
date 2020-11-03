using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class Gc {
        public string AutoManual { get; set; }
        public string ZtPlcStatus { get; set; }
        public string AppControlStatus { get; set; }
        public string CycleCount { get; set; }
        public string PlTimeSecond { get; set; }
        public string CycleMode { get; set; }
        public string WorkDam { get; set; }
        public string GunCountPerGroup { get; set; }
        public string PlTimeRemaining { get; set; }
        public string CycleEndStopPump { get; set; }
        public string CurrentWorkingDam { get; set; }
        public string CurrentDoneCycleCount { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public Gc Instance {
            get {
                if (_instance == null) {
                    var json = File.ReadAllText("gc.json");
                    _instance = JsonConvert.DeserializeObject<Gc>(json);
                }
                return _instance;
            }
        } static private Gc _instance = null;

    }
}
