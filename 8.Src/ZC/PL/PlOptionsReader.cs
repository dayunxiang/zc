using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class PlOptionsReader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PlOptions Read()
        {
            // todo:
            return new PlOptions()
            {
                CycleMode = CycleMode.AllDam,
                CycleTimes = 2,
                PlTimeSpan = TimeSpan.FromSeconds(5),
                GunCountPerGroup = 4,
                WorkDam = 0,
            };
        }
    }
}
