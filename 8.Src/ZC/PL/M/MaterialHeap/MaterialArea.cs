using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;

namespace PL {

    public class MaterialArea {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        public MaterialArea(MaterialAreaDefine define) {
            if (define == null) {
                throw new ArgumentNullException("materialAreaDefine");
            }

            this.Define = define;
            this.MaterialHeapPositions = new MaterialHeapPositionList();
        }

        public MaterialAreaDefine Define {
            get;
            private set;
        }

        public MaterialHeapPositionList MaterialHeapPositions {
            get;
            private set;
        }
    }
}
