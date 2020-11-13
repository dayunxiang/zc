//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Threading;
//using NLog;

//namespace PLC {

//    public class Service : IService {
//        static private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="obj"></param>
//        static private void Debug(object obj) {
//            _logger.Debug(obj);
//            //Console.WriteLine(obj);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        private Opc.Da.Server _server;
//        private Task _task;
//        private CancellationTokenSource _tokenSource;
//        private CancellationToken _token;

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public bool IsConnected() {
//            return _server != null && _server.IsConnected;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        private void LoopRead() {
//            try {
//                if (_task != null && _task.IsCanceled == false) {
//                    while (true) {
//                        _token.ThrowIfCancellationRequested();

//                        Read();
//                        const int sleep = 1000;
//                        Thread.Sleep(sleep);
//                    }
//                }
//            } catch (Exception ex) {
//                Debug(ex);
//                _task = null;
//                _tokenSource = null;
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        private void Read() {
//            try {
//                Opc.Da.ItemValueResult[] itemValueResults = _server.Read(GetItems());

//                foreach (Opc.Da.ItemValueResult ivr in itemValueResults) {
//                    if (ivr.ResultID == Opc.ResultID.S_OK) {
//                        //ItemDefine itemDefine = this.ItemDefines.SetValue(ivr.ItemPath, ivr.ItemName, ivr.Value);
//                        this.ItemDefines.SetValue(ivr.ItemPath, ivr.ItemName, ivr.Value);
//                    } else {

//                    }
//                }
//            } catch (Exception ex) {
//                Debug(ex);
//            }
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        private void TestRead()//CancellationToken token)
//        {
//            try {
//                if (_task != null && _task.IsCanceled == false) {
//                    while (true) {
//                        ItemDefines.SetValue("", "abc", DateTime.Now);
//                        Debug("read()...");
//                        _token.ThrowIfCancellationRequested();

//                        const int sleep = 1000;
//                        Thread.Sleep(sleep);
//                    }
//                }
//            } catch (Exception ex) {
//                Debug(ex);
//                _task = null;
//                _tokenSource = null;
//                //_token = null;
//            }

//            //_task = null;
//            //_tokenSource = null;
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        public void StartLoop() {
//            if (_task == null) {
//                Action action = TestRead; // LoopRead
//                _tokenSource = new CancellationTokenSource();
//                _token = _tokenSource.Token;
//                _task = new Task(action, _token);
//                _task.Start();
//            } else {
//                throw new InvalidOperationException("started");
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        public void StopLoop() {
//            if (_task != null) {
//                _tokenSource.Cancel();
//                //_tokenSource.Token.ThrowIfCancellationRequested();
//                //_task = null;
//                //_tokenSource = null;
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public bool IsLoop() {
//            return _task != null;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public Opc.Da.ServerStatus GetStatus() {
//            if (_server != null) {
//                return _server.GetStatus();
//            } else {
//                return null;
//            }
//        }


//        /// <summary>
//        /// 
//        /// </summary>
//        public bool Connect() {
//            _server = new Opc.Da.Server(GetFactory(), GetConnectUrl());
//            try {
//                _server.Connect(GetConnectData());
//            } catch (Exception ex) {
//                Debug(ex);
//                _server = null;
//            }

//            return _server != null;
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private Opc.Factory GetFactory() {
//            return new OpcCom.Factory();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private Opc.ConnectData GetConnectData() {
//            return new Opc.ConnectData(null, null);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        private Opc.URL GetConnectUrl() {
//            var machine = "localhost";
//            var url = string.Format(
//                    "opcda://{0}/RSLinx OPC Server/{a05bb6d5-2f8a-11d1-9bb0-080009d01446}",
//                    machine
//                    );
//            return new Opc.URL(url);
//        }



//        /// <summary>
//        /// 
//        /// </summary>
//        public ItemDefineList ItemDefines {
//            get {
//                if (_itemDefines == null) {
//                    _itemDefines = new ItemDefineList();
//                }
//                return _itemDefines;
//            }
//            set {
//                if (_itemDefines != value) {
//                    _itemDefines = value;
//                    _items = null;
//                }
//            }
//        } private ItemDefineList _itemDefines;

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        public Opc.Da.Item[] GetItems() {
//            if (_items == null) {
//                _items = this.ItemDefines.Create();
//            }
//            return _items;
//        } private Opc.Da.Item[] _items;
//    }
//}
