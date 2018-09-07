using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLC
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleOpcServer
    {

        private Opc.Da.Server _server;
        private ItemCache _itemCache;

        /// <summary>
        /// 
        /// </summary>
        public SimpleOpcServer()
        {
            _itemCache = new ItemCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return _server != null && _server.IsConnected;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.Factory GetFactory()
        {
            return new OpcCom.Factory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.ConnectData GetConnectData()
        {
            return new Opc.ConnectData(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.URL GetConnectUrl(string machine)
        {
            //var url = string.Format(
            //        @"opcda://{0}/RSLinx OPC Server/{a05bb6d5-2f8a-11d1-9bb0-080009d01446}",
            //        machine
            //        );
            var url = "opcda://" + machine + "/RSLinx OPC Server/{a05bb6d5-2f8a-11d1-9bb0-080009d01446}";
            return new Opc.URL(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool Connect()
        {
            _server = new Opc.Da.Server(GetFactory(), GetConnectUrl("localhost"));
            Lm.D("Connect...");
            try
            {
                _server.Connect(GetConnectData());
            }
            catch (Exception ex)
            {
                Debug(ex);
                _server = null;
            }

            bool success = _server != null && _server.IsConnected;
            Lm.D("Connect: " + success);
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect()
        {
            if (_server != null)
            {
                try
                {
                    _server.Disconnect();
                }
                catch (Exception ex)
                {
                    Debug(ex);
                }
                _server.Dispose();
                _server = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        private void Debug(Exception ex)
        {
            Console.WriteLine(ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public object Read(string itemName)
        {
            var values = Read(new[] { itemName });
            return values[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        /// <returns></returns>
        public object[] Read(string[] itemNames)
        {
            List<Opc.Da.Item> items = new List<Opc.Da.Item>(itemNames.Length);
            foreach( var itemName in itemNames)
            {
                var item = _itemCache.Get(itemName);
                items.Add(item);
            }

            var itemValueResults = Read(items.ToArray());
            List<object> values = new List<object> (itemNames.Length);
            foreach( var itemValueResult in itemValueResults)
            {
                //itemValueResult.Quality
                values.Add(itemValueResult.Value);
            }
            return values.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public Opc.Da.ItemValueResult[] Read(Opc.Da.Item[] items)
        {
            Opc.Da.ItemValueResult[] results = _server.Read(items);
            LogReadInfo(items, results);
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="results"></param>
        private void LogReadInfo(Opc.Da.Item[] items, Opc.Da.ItemValueResult[] results)
        {
            var sb = new StringBuilder();
            sb.Append("read: ");
            for (int i = 0; i < items.Length ; i++)
            {
                sb.AppendFormat("{0}->{1}({2})", items[i].ItemName, results[i].Value, results[i].ResultID);
            }
            Lm.D(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult Write(string itemName, object value)
        {
            var s = Write(new string[] { itemName }, new object[] { value });
            return s[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult[] Write(string[] itemNames, object[] values)
        {
            List<Opc.Da.ItemValue> itemValues = new List<Opc.Da.ItemValue>();

            for (int i = 0; i < itemNames.Length; i++)
            {
                var itemName = itemNames[i];
                var value = values[i];

                var itemValue = new Opc.Da.ItemValue(itemName);
                itemValue.Value = value;
                itemValues.Add(itemValue);
            }

            var identifiedResults = Write(itemValues.ToArray());
            return identifiedResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemValues"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult[] Write(Opc.Da.ItemValue[] itemValues)
        {
            var r = _server.Write(itemValues);
            LogWriteInfo(itemValues, r);
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogWriteInfo(Opc.Da.ItemValue[] itemValues, Opc.IdentifiedResult[] results)
        {
            var sb = new StringBuilder();
            sb.Append("write: ");
            for (int i = 0; i < itemValues.Length; i++)
            {
                sb.AppendFormat("{0}({1})->{2}, ", 
                    itemValues[i].ItemName, 
                    itemValues[i].Value, 
                    results[i].ResultID);
            }
            Lm.D(sb.ToString());
        }


        /// <summary>
        /// 
        /// </summary>
        internal class ItemCache
        {
            private Dictionary<string, Opc.Da.Item> _dict = new Dictionary<string, Opc.Da.Item>(1000);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="itemName"></param>
            /// <returns></returns>
            public Opc.Da.Item Get(string itemName)
            {
                if (_dict.ContainsKey(itemName))
                {
                    return _dict[itemName];
                }
                else
                {
                    var identifier = new Opc.ItemIdentifier(itemName);
                    var item = new Opc.Da.Item(identifier);
                    _dict[itemName] = item;
                    return item;
                }
            }
        }
    }
}
