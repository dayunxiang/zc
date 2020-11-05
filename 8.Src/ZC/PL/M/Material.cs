using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL {
    public class Material {
        public static Material Empty = new Material("Empty");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Material(string name)
            : this(name, true) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Material(string name, bool canWet) {
            this.Name = name;
            this.CanWet = canWet;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool CanWet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return string.Format(
                "{{Name={0}, CanWet={1}}}", 
                this.Name, 
                this.CanWet);
        }
    }
}
