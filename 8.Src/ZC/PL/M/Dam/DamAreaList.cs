using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using PL.Hardware;

namespace PL {

    public class DamAreaList : List<DamArea> {

        public DamArea GetByName(string damAreaName) {
            if(string.IsNullOrEmpty(damAreaName)) {
                throw new ArgumentException("damAreaName null or empty");
            }

            var da = this.FirstOrDefault(
                damArea =>
                damArea.Define.Name.Trim().ToUpper() ==
                damAreaName.Trim().ToUpper());

            if (da == null) {
                var s = string.Format("can not find dam area by name '{0}'", damAreaName);
                throw new ArgumentException(s);
            }
            return da;
        }
    }
}
