using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL {

    public class Area {
        public static readonly Area Empty = new Area();


        /// <summary>
        /// 
        /// </summary>
        public Area() {
        }

        #region Material
        /// <summary>
        /// 
        /// </summary>
        public Material Material {
            get {
                if (_material == null) {
                    _material = Material.Empty;
                }
                return _material;
            }
            set {
                if (_material != value) {
                    _material = value;
                }
            }
        } private Material _material;
        #endregion //Material

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format(
                "{{Material={0}}}",
                this.Material);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanWet() {
            return this.Material.CanWet;
        }
    }
}
