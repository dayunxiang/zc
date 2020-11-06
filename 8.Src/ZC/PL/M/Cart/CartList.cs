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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal Cart GetByName(string cartName) {
            var comparer = StringComparer.OrdinalIgnoreCase;
            var r = this.First(cart => comparer.Compare(cart.Name, cartName) == 0);
            return r;
        }
    }
}
