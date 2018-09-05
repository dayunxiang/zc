using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public enum ControllerStatusEnum
    {
        /// <summary>
        /// 
        /// </summary>
        NotRun = 0,
        /// <summary>
        /// 
        /// </summary>
        Idle = 1,
        /// <summary>
        /// 
        /// </summary>
        Working = 2,
        /// <summary>
        /// 
        /// </summary>
        Completed = 3,
    }
}
