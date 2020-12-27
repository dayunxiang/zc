using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using PLC;
using PL.Hardware;

namespace PL {

    public class Cart {

        /// <summary>
        /// 
        /// </summary>
        private Logger _logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// 
        /// </summary>
        private CartDefine _cartDefine;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        public Cart(CartDefine define){
            if (define == null) {
                throw new ArgumentNullException("define");
            }
            this._cartDefine = define;
        }

        /// <summary>
        /// 
        /// </summary>
        public CartDefine CartDefine {
            get { return _cartDefine; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get {
                return _cartDefine.Name;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int No {
            get {
                return _cartDefine.No;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Address {
            get {
                return _cartDefine.Address;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string FaultAddress {
            get {
                return _cartDefine.FaultAddress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal ReadLocation() {
            var val = OpcServerManager.Instance.OpcServer.Read(_cartDefine.Address);
            return Convert.ToDecimal(val);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ReadFault() {
            var val = OpcServerManager.Instance.OpcServer.Read(_cartDefine.FaultAddress);
            return Convert.ToBoolean(val);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            var f = "{{No={0}, Name={1}, Address={2}, Location={3}}}";
            return string.Format(
                f,
                this.No,
                this.Name,
                this._cartDefine.Address,
                this.ReadLocation());
        }
    }
}
