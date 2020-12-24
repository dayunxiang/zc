using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC {

    public class Items : List<Item> {

        public void SetGcValues() {
            Set("[a]Global_Control.AutoManual", 1);
            //"ZtPlcStatus" : "[a]Global_Control.ztplcstatus",
            //"AppControlStatus" : "[a]Global_Control.AppControlStatus",
            Set("[a]Global_Control.CycleCount", 2);
            Set("[a]Global_Control.PLtimeSecond", 300);
            //"CycleMode" : "[a]Global_Control.CycMode",
            //"WorkDam" : "[a]Global_Control.WorkDam",
            Set("[a]Global_Control.GunCountPerGroup", 6);
            //"PlTimeRemaining" : "[a]Global_Control.PLtimeremaining",
            //"CycleEndStopPump" : "[a]Global_Control.CycEndStoppump",
            //"CurrentWorkingDam" : "[a]Global_Control.CurrentWorkingDam",
            //"CurrentDoneCycleCount" : "[a]Global_Control.CurrentDoneCycleCount"
            Set("[a]StockGPS[0].GroundID_String", "2A");
            Set("[a]StockGPS[1].GroundID_String", "2B");
            Set("[a]StockGPS[2].GroundID_String", "2C");
            Set("[a]StockGPS[3].GroundID_String", "2D");
            Set("[a]StockGPS[4].GroundID_String", "2E");
            Set("[a]StockGPS[5].GroundID_String", "2F");
            Set("[a]StockGPS[6].GroundID_String", "2G");
            Set("[a]StockGPS[7].GroundID_String", "2H");

            //// set remote = 1, can not use
            ////
            //this.ForEach(a => {
            //    if (a.Name.ToUpper().Contains("REMOTE")) {
            //        a.Value = 1;
            //    }
            //});

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="n"></param>
        private void Set(string name, object n) {
            foreach (var item in this) {
                if (StringComparer.OrdinalIgnoreCase.Equals(item.Name, name)) {
                    item.Value = n;
                }
            }
        }
    }
}
