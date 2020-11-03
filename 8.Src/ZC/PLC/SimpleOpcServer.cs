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
    public class SimpleOpcServer {
        static private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private Opc.Da.Subscription _subscription;
        private Opc.Da.Server _server;
        private ItemCache _itemCache;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler ConnectedEvent;

        /// <summary>
        /// 
        /// </summary>
        public SimpleOpcServer() {
            _itemCache = new ItemCache();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected() {
            return _server != null && _server.IsConnected;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.Factory GetFactory() {
            return new OpcCom.Factory();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.ConnectData GetConnectData() {
            return new Opc.ConnectData(null, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.URL GetConnectUrl(string machine) {
            var url = "opcda://" + machine + "/RSLinx OPC Server/{a05bb6d5-2f8a-11d1-9bb0-080009d01446}";
            return new Opc.URL(url);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool Connect() {
            _server = new Opc.Da.Server(GetFactory(), GetConnectUrl("localhost"));
            try {
                _server.Connect(GetConnectData());

                // create subscription state
                var subscriptionState = new Opc.Da.SubscriptionState();
                subscriptionState.ClientHandle = null;
                subscriptionState.ServerHandle = null;
                subscriptionState.Name = "OPCSample";
                subscriptionState.Active = false;
                subscriptionState.UpdateRate = 1000;
                subscriptionState.KeepAlive = 0;
                subscriptionState.Deadband = 0;
                subscriptionState.Locale = null;
                subscriptionState.ClientHandle = Guid.NewGuid().ToString();

                _subscription = (Opc.Da.Subscription)_server.CreateSubscription(subscriptionState);
            } catch (Exception ex) {
                Debug(ex);
                _server = null;
            }

            bool success = _server != null && _server.IsConnected;
            MyLogManager.Output("Connect: " + success);

            if (success) {
                OnConnected();
            }
            return success;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnConnected() {
            if (ConnectedEvent != null) {
                ConnectedEvent(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        public void AddSubscriptionItems(string[] itemNames) {
            _logger.Debug("AddSubscriptionItems - begin");

            _itemCache.Clear();
            var items = CreateSubscriptionItems(itemNames);
            var itemResults = _subscription.AddItems(items);

            _itemCache.Set(_subscription.Items);

            foreach (var itemResult in itemResults) {
                _logger.Debug("ItemName: {0}, ResultId: {1}",
                    itemResult.ItemName,
                    itemResult.ResultID.ToString());
            }
            _logger.Debug("AddSubscriptionItems - end");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Opc.Da.Item[] CreateSubscriptionItems(string[] itemNames) {
            var r = new List<Opc.Da.Item>();
            foreach (var itemName in itemNames) {
                var itemId = new Opc.ItemIdentifier(itemName);
                var item = new Opc.Da.Item(itemId);
                r.Add(item);
            }

            return r.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Disconnect() {
            if (_server != null) {
                try {
                    _server.Disconnect();
                } catch (Exception ex) {
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
        private void Debug(Exception ex) {
            MyLogManager.Output(ex.ToString());
            Console.WriteLine(ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public object Read(string itemName) {
            var values = Read(new[] { itemName });
            return values[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        /// <returns></returns>
        public object[] Read(string[] itemNames) {
            var items = _itemCache.Get(itemNames);
            var itemValueResults = Read(items);
            List<object> values = new List<object>(itemNames.Length);
            foreach (var itemValueResult in itemValueResults) {
                //itemValueResult.Quality
                values.Add(itemValueResult.Value);
            }
            return values.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private Opc.Da.ItemValueResult[] Read(Opc.Da.Item[] items) {
            // use subsctiption read
            //
            return ReadFromSubscription(items);

            // with server.read
            //
            //Opc.Da.ItemValueResult[] results = _server.Read(items);
            //LogReadInfo(items, results);
            //return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        private Opc.Da.ItemValueResult[] ReadFromSubscription(Opc.Da.Item[] items) {
            if (items == null) {
                throw new ArgumentNullException();
            }

            if (items.Length == 0) {
                throw new ArgumentException("items length == 0");
            }

            if (IsConnected()) {
                Opc.Da.ItemValueResult[] results = null;
                try {
                    results = _subscription.Read(items);
                    LogReadInfo(items, results);
                    return results;
                } catch (Opc.ResultIDException resultIdEx) {
                    //Lm.D(resultIdEx.ToString ());
                    //return new Opc.Da.ItemValueResult[0];
                    var msg = string.Format("read opc items '{0}' count '{1}', fail '{2}'",
                        items[0].ItemName,
                        items.Length,
                        resultIdEx.Result);
                    throw new OpcException(msg, resultIdEx);
                }
            } else {
                //return new Opc.Da.ItemValueResult[0];
                throw new InvalidOperationException("opc not connect");
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="items"></param>
        ///// <returns></returns>
        //private Opc.Da.Item[] FindSubscriptionItems(Opc.Da.Item[] items)
        //{
        //    var r= new List<Opc.Da.Item>();
        //    foreach (var item in items)
        //    {
        //        foreach (var subItem in _subscription.Items)
        //        {
        //            if(subItem.ItemName == item.ItemName)
        //            {
        //                r.Add(subItem);
        //                break;
        //            }
        //        }
        //    }
        //    return r.ToArray ();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="results"></param>
        private void LogReadInfo(Opc.Da.Item[] items, Opc.Da.ItemValueResult[] results) {
            var sb = new StringBuilder();
            sb.Append("read: ");
            for (int i = 0; i < items.Length; i++) {
                sb.AppendFormat("{0}->{1}({2})", items[i].ItemName, results[i].Value, results[i].ResultID);
            }
            MyLogManager.Output(sb.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult Write(string itemName, object value) {
            var s = Write(new string[] { itemName }, new object[] { value });
            return s[0];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemNames"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult[] Write(string[] itemNames, object[] values) {
            List<Opc.Da.ItemValue> itemValues = new List<Opc.Da.ItemValue>();

            for (int i = 0; i < itemNames.Length; i++) {
                var itemName = itemNames[i];
                var value = values[i];

                var item = _itemCache.Get(itemName);

                var itemValue = new Opc.Da.ItemValue(item);
                itemValue.Value = value;
                itemValues.Add(itemValue);
            }

            var identifiedResults = Write(itemValues.ToArray());
            return identifiedResults;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="itemNames"></param>
        ///// <param name="values"></param>
        ///// <returns></returns>
        //public Opc.IdentifiedResult[] WriteWithSub(string[] itemNames, object[] values)
        //{

        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemValues"></param>
        /// <returns></returns>
        public Opc.IdentifiedResult[] Write(Opc.Da.ItemValue[] itemValues) {
            if (IsConnected()) {
                try {
                    var r = _subscription.Write(itemValues);
                    LogWriteInfo(itemValues, r);
                    return r;
                } catch (Exception ex) {
                    //Lm.D(ex.ToString());
                    //return new Opc.IdentifiedResult[0];
                    var msg = string.Format("write item '{0}' count '{1}' fail",
                        itemValues[0].ItemName, itemValues.Length);
                    throw new OpcException(msg, ex);
                }
            } else {
                //return new Opc.IdentifiedResult[0];
                throw new InvalidOperationException("opc not connect");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LogWriteInfo(Opc.Da.ItemValue[] itemValues, Opc.IdentifiedResult[] results) {
            var sb = new StringBuilder();
            sb.Append("write: ");
            for (int i = 0; i < itemValues.Length; i++) {
                sb.AppendFormat("{0}({1})->{2}, ",
                    itemValues[i].ItemName,
                    itemValues[i].Value,
                    results[i].ResultID);
            }
            MyLogManager.Output(sb.ToString());
        }

    }
}
