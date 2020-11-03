using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC {

    /// <summary>
    /// 
    /// </summary>
    internal class ItemCache {
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, Opc.Da.Item> _dict = new Dictionary<string, Opc.Da.Item>(1000);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public Opc.Da.Item Get(string itemName) {
            if (_dict.ContainsKey(itemName)) {
                return _dict[itemName];
            } else {
                var s = string.Format("cannot find item by name '{0}'", itemName);
                throw new ArgumentException(s);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        /// <returns></returns>
        public Opc.Da.Item[] Get(string[] itemNames) {
            List<Opc.Da.Item> r = new List<Opc.Da.Item>();
            foreach (var itemName in itemNames) {
                var item = Get(itemName);
                r.Add(item);
            }
            return r.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="item"></param>
        public void Set(string itemName, Opc.Da.Item item) {
            this._dict[itemName] = item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void Set(Opc.Da.Item[] items) {
            foreach (var item in items) {
                this.Set(item.ItemName, item);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        internal void Clear() {
            this._dict.Clear();
        }
    }
}
