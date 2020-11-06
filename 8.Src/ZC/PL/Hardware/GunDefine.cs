using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PL.Hardware {

    public class GunDefine {
        public int No { get; set; }
        public string Name { get; set; }
        public string Switch { get; set; }
        public string Mark { get; set; }
        public string Fault { get; set; }
        public string Remote { get; set; }
        public decimal Location { get; set; }
        public string AssociateCartName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal Gun Create(CartList carts) {
            if (carts == null) {
                throw new ArgumentNullException("carts");
            }
            return new Gun() {
                No = this.No,
                Name = this.Name,
                Fault = new Fault(this.Fault),
                Mark = new Mark(this.Mark),
                Remote = new Remote(this.Remote),
                Switch = new Switch(this.Switch),
                Location = this.Location,
                AssociateCart = carts.GetByName(this.AssociateCartName),
            };
        }

    }
}
