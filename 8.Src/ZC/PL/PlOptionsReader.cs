using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class PlOptionsReader {

        private App _app;

        /// <summary>
        /// 
        /// </summary>
        public PlOptionsReader(App app) {
            _app = app;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PlOptions Read() {
            return ReadFact();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private PlOptions ReadMock()
        //{
        //    return new PlOptions()
        //    {
        //        CycleMode = CycleModeEnum.AllDam,
        //        CycleTimes = 2,
        //        PlTimeSpan = TimeSpan.FromSeconds(5),
        //        GunCountPerGroup = 4,
        //        WorkDam = 0,
        //    };
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlOptions ReadFact() {
            var gc = _app.Gc;
            var opcServer = OpcServerManager.Instance.OpcServer;

            List<string> itemNames = new List<string>();
            itemNames.Add(gc.CycleCount);
            itemNames.Add(gc.CycleMode);
            itemNames.Add(gc.GunCountPerGroup);
            itemNames.Add(gc.PlTimeSecond);
            itemNames.Add(gc.WorkDam);
            var values = opcServer.Read(itemNames.ToArray());

            var plOptions = new PlOptions(this._app);
            plOptions.CycleTimes = Convert.ToInt32(values[0]);
            plOptions.CycleMode = (CycleModeEnum)Convert.ToInt32(values[1]);
            plOptions.GunCountPerGroup = Convert.ToInt32(values[2]);
            plOptions.PlTimeSpan = TimeSpan.FromSeconds(Convert.ToInt32(values[3]));
            plOptions.WorkDam = Convert.ToInt32(values[4]);
            return plOptions;
        }
    }
}
