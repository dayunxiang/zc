using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC
{

    public class ItemDefine
    {
        public string ItemPath { get; set; }
        public string ItemName { get; set; }
        public object Tag { get; set; }
        public object ItemValue
        {
            get { return _itemValue; }
            set
            {
                if (_itemValue != value)
                {
                    _itemValue = value;
                }
            }
        } private object _itemValue;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Opc.Da.Item Create()
        {
            Opc.ItemIdentifier itemIdentifier = new Opc.ItemIdentifier(this.ItemPath, this.ItemName);
            Opc.Da.Item item = new Opc.Da.Item(itemIdentifier);
            return item;
        }
    }
}
