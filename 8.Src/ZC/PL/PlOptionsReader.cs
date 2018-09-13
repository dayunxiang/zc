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
        public PlOptionsReader()
        {

        }

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
            var gc = App.GetApp().Gc;
            var opcServer = App.GetApp().Opc;
            List<string> itemNames = new List<string>();
            itemNames.Add(gc.CycleCount);
            itemNames.Add(gc.CycleMode);
            itemNames.Add(gc.GunCountPerGroup);
            itemNames.Add(gc.PlTimeSecond);
            itemNames.Add(gc.WorkDam);
            var values = opcServer.Read(itemNames.ToArray());

            var plOptions = new PlOptions();
            plOptions.CycleTimes = Convert.ToInt32(values[0]);
            plOptions.CycleMode = (CycleMode)Convert.ToInt32(values[1]);
            plOptions.GunCountPerGroup = Convert.ToInt32(values[2]);
            plOptions.PlTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(values[3]));
            plOptions.WorkDam = Convert.ToInt32(values[4]);
            return plOptions;
        }
    }
}
