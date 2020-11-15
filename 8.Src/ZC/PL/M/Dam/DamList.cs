using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    /// <summary>
    /// 
    /// </summary>
    public class DamList : List<Dam> {
        public DamList() {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        public DamList(IEnumerable<Dam> collection)
            : base(collection) {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public WorkGunGroup GetFirstGuns(int count) {
            WorkGunGroup result = null;
            foreach (var dam in this) {
                if (result == null) {
                    result = dam.GetFirstGuns(count);
                }
                else {
                    var part = dam.GetFirstGuns(count - result.WorkGuns.Count);
                    result.Merge(part);
                }

                if (result.WorkGuns.Count == count) {
                    break;
                }
            }

            return result;
        }
    }
}
