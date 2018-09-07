using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Config
    {
        /// <summary>
        /// 
        /// </summary>
        static Config()
        {
            DiscardGunsCloseDelay = 3;
            CheckInterval = 2000;
            IsMock = true;
        }

        /// <summary>
        /// 
        /// </summary>
        static public int DiscardGunsCloseDelay
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        static public int CheckInterval
        {
            get;
            set;
        }

        static public bool IsMock
        {
            get;
            set;
        }
    }
}
