using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class PlOptions
    {
        public int CycleTimes { get; set; }
        public TimeSpan PlTimeSpan { get; set; }
        public CycleMode CycleMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// valid when CycleMode is SelectedDam
        /// bit 0 - dam 0 selected
        /// bit 1 - dam 1 selected
        /// ...
        /// </remarks>
        public int WorkDam { get; set; }

        public int GunCountPerGroup { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextDam"></param>
        /// <returns></returns>
        internal bool IsWorkDam(Dam dam)
        {
            throw new NotImplementedException();
        }
    }
}
