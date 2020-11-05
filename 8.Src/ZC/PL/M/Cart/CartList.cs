using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PL {

    public class CartList : List<Cart> {

        /// <summary>
        /// 
        /// </summary>
        public void RefreshLocations() {
            this.ForEach(cart => cart.RefreshLocation());
        }
    }
}
