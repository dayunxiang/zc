using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class DamLinkedList : LinkedList<Dam> {
        /// <summary>
        /// 
        /// </summary>
        public DamLinkedList() {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public WorkGunGroup GetFirstGuns(PlOptions options) {
            //Dam firstDam = this.First.Value;
            //var workDam = GetWorkDam(firstDam, options);
            //return workDam.GetFirstGuns(options.GunCountPerGroup);

            DamList workDams = GetWorkDams(options);
            WorkGunGroup workGunGroup = workDams.GetFirstGuns(options.GunCountPerGroup);

            Debug.Assert(options.GunCountPerGroup == workGunGroup.WorkGuns.Count);
            return workGunGroup;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dam"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private Dam GetWorkDam(Dam dam, PlOptions options) {
            if (options.IsWorkDam(dam)) {
                return dam;
            } else {
                var nextDam = dam.GetNextDam();
                return GetWorkDam(nextDam, options);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plOptions"></param>
        /// <returns></returns>
        internal DamList GetWorkDams(PlOptions plOptions) {
            if (plOptions.CycleMode == CycleModeEnum.AllDam) {
                return new DamList(this.ToArray());
            } else {
                var r = new DamList();

                int[] mask = new int[] { 1, 2, 4, 8 };
                int n = 0;
                foreach (var dam in this) {
                    bool isSelected = (plOptions.WorkDam & mask[n]) > 0;
                    if (isSelected) {
                        r.Add(dam);
                    }
                    n++;
                }
                return r;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="damValue"></param>
        /// <returns></returns>
        public Dam FindDamByValue(int damValue) {
            // todo: find dam by value
            //
            foreach (var dam in this) {
                if (dam.No == damValue) {
                    return dam;
                }
            }

            return null;
        }
    }
}
