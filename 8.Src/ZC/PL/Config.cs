using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {
    public class Config {
        /// <summary>
        /// 
        /// </summary>
        static Config() {
            DiscardGunsCloseDelay = 4;
            CheckInterval = 2000;
            GunsCloseDelaySecondWhenStopPump = 10;
            //CartRange = 75m;
            GunRadius = 40m;

            IsMock = false;
        }

        /// <summary>
        /// 
        /// </summary>
        static public int DiscardGunsCloseDelay {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        static public int CheckInterval {
            get;
            set;
        }

        static public bool IsMock {
            get;
            set;
        }

        static public int GunsCloseDelaySecondWhenStopPump {
            get;
            set;
        }

        //public static decimal CartRange {
        //    get;
        //    set;
        //}

        public static decimal GunRadius {
            get;
            set;
        }
    }
}
