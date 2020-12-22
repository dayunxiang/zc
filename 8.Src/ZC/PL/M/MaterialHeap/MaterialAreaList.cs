using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {

    public class MaterialAreaList : List<MaterialArea>{
        public MaterialArea GetByName(string materialAreaName) {
            materialAreaName = materialAreaName.Trim ();

            var ma =  this.First(x => {
                var maName = x.ReadStockGroupIdString();
                maName = maName.Trim();
                return StringComparer.OrdinalIgnoreCase.Compare(maName, materialAreaName) == 0;
            });

            return ma;
        }
    }
}
