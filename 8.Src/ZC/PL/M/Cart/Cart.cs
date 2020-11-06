using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PL {

    public class Cart : PlcAddress {

        /// <summary>
        /// 
        /// </summary>
        private Logger _logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 
        /// </summary>
        public EventHandler<CartLocationEventArgs> Changed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Cart(string address)
            : base(address) {

        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Location {
            get { return _location; }
            set {
                if (value <= 0M) {
                    _logger.Error("Cart.Location must >= 0, current is: " + value);
                }

                if (_location != value) {
                    _location = value;
                    OnLocationChanged();
                }
            }
        } private decimal _location = 0M;


        /// <summary>
        /// 
        /// </summary>
        protected void OnLocationChanged() {
            if (this.Changed != null) {
                var e = new CartLocationEventArgs(this, this.Location);
                this.Changed(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public void RefreshLocation() {
            var val = base.ReadFromOpc();
            int intValue = Convert.ToInt32(val);
            this.Location = intValue;
        }

        public override string ToString() {
            var f = "{{No={0}, Name={1}, Address={2}, Location={3}}}";
            return string.Format(
                f,
                this.No,
                this.Name,
                this.Address,
                this.Location);
        }
    }
}
