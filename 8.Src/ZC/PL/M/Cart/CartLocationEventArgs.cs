//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using NLog;

//namespace PL {

//    public class CartLocationEventArgs : EventArgs {
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="cart"></param>
//        /// <param name="location"></param>
//        public CartLocationEventArgs(Cart cart, decimal location) {
//            if (cart == null) {
//                throw new ArgumentNullException("cart");
//            }

//            this.Cart = cart;
//            this.Location = location;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public Cart Cart { get; private set; }

//        /// <summary>
//        /// 
//        /// </summary>
//        public decimal Location { get; private set; }
//    }
//}
