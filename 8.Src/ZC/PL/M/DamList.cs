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
    }
}
