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
        }

        /// <summary>
        /// 
        /// </summary>
        static public int DiscardGunsCloseDelay
        {
            get;
            set;
        }
    }
}
