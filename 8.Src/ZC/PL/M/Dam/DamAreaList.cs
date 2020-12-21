using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using PL.Hardware;

namespace PL {

    public class DamAreaList : List<DamArea> {

        public DamArea GetByName(string damAreaName) {
            var r = this.First(
                damArea =>
                damArea.Define.Name.Trim().ToUpper() ==
                damAreaName.Trim().ToUpper());
            return r;
        }
    }
}
