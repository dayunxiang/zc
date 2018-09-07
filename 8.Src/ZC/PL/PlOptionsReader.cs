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
            if (Config.IsMock)
            {
                return ReadMock();
            }
            else
            {
                return ReadFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlOptions ReadMock()
        {
            return new PlOptions()
            {
                CycleMode = CycleMode.AllDam,
                CycleTimes = 2,
                PlTimeSpan = TimeSpan.FromSeconds(5),
                GunCountPerGroup = 4,
                WorkDam = 0,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlOptions ReadFact()
        {
            throw new NotImplementedException();
        }


    }
}
