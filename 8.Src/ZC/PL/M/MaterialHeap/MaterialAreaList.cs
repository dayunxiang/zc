using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {

    public class MaterialAreaList : List<MaterialArea> {
        public MaterialArea GetByName(string materialAreaName) {
            if (string.IsNullOrEmpty(materialAreaName)) {
                throw new ArgumentException("materialAreaName");
            }

            materialAreaName = materialAreaName.Trim();

            var ma = this.FirstOrDefault(x => {
                var maName = x.ReadStockGroupIdString();
                maName = maName.Trim();
                return StringComparer.OrdinalIgnoreCase.Compare(maName, materialAreaName) == 0;
            });

            if (ma == null) {
                var s = string.Format("can not find material area by name '{0}'", materialAreaName);
                throw new ArgumentException (s);
            }
            return ma;
        }
    }
}
