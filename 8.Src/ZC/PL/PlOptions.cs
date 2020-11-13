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
        public CycleModeEnum CycleMode { get; set; }
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
            var workDams = GetWorkDams();
            return workDams.Contains(dam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_openDt"></param>
        /// <returns></returns>
        internal bool IsTimeout(DateTime openDateTime)
        {
            var ts = DateTime.Now - openDateTime;
            return ts < TimeSpan.Zero || ts >= PlTimeSpan;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_plOptions"></param>
        /// <returns></returns>
        internal DamList GetWorkDams()
        {
            var dams = App.GetApp().Dams;

            if (this.CycleMode == CycleModeEnum.AllDam)
            {
                return new DamList(dams.ToArray());
            }
            else
            {
                var r = new DamList();

                int[] mask = new int[] { 1, 2, 4, 8 };
                int n = 0;
                foreach (var dam in dams)
                {
                    bool isSelected = (this.WorkDam & mask[n]) > 0;
                    if (isSelected)
                    {
                        r.Add(dam);
                    }
                    n++;
                }
                return r;
            }
        }
    }
}
