using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC
{

    public class ItemDefineList : List<ItemDefine>
    {
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Opc.Da.Item[] Create()
        {
            List<Opc.Da.Item> items = new List<Opc.Da.Item>();
            foreach (var itemDefine in this)
            {
                items.Add(itemDefine.Create());
            }
            return items.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemPath"></param>
        /// <param name="itemName"></param>
        /// <returns></returns>
        internal ItemDefine Find(string itemPath, string itemName)
        {
            var cmp = StringComparer.CurrentCultureIgnoreCase;
            return this.First((x) =>
                    {
                    return cmp.Compare(x.ItemPath, itemPath) == 0 &&
                    cmp.Compare(x.ItemName, itemName) == 0;
                    });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemPath"></param>
        /// <param name="itemName"></param>
        /// <param name="value"></param>
        public void SetValue(string itemPath, string itemName, object value)
        {
            var itemDefine = Find(itemPath, itemName);
            itemDefine.ItemValue = value;
            FireValueChanged(itemDefine);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemDefine"></param>
        private void FireValueChanged(ItemDefine itemDefine)
        {
            if( ValueChanged != null)
            {
                ValueChanged(this, new ValueChangedEventArgs(itemDefine));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach( var i in this)
            {
                sb.AppendFormat("{0}\r\n", i.ToString());
            }
            return sb.ToString();
        }
    }
}
