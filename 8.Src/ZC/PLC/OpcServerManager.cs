using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC {

    public class OpcServerManager {

        public static readonly OpcServerManager Instance = new OpcServerManager();

        /// <summary>
        /// 
        /// </summary>
        private OpcServerManager() {
        }

        public bool IsMock { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IOpcServer OpcServer {
            get {
                if (_opcServer == null) {
                    bool b = TryConnect();
                    if (!b) {
                        throw new OpcException("get opc server fail");
                    }
                }
                return _opcServer;
            }
            set {
                _opcServer = value;
            }
        }  private IOpcServer _opcServer;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected() {
            return this._opcServer != null &&
                this._opcServer.IsConnected();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool TryConnect() {
            if (IsMock) {
                _opcServer = CreateMockOpcServer();
            }
            else {
                _opcServer = CreateAndConnectSimpleOpcServer();
            }

            return _opcServer != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IOpcServer CreateAndConnectSimpleOpcServer() {
            var simpleOpcServer = new SimpleOpcServer();
            if (simpleOpcServer.Connect()) {
                return simpleOpcServer;
            }
            else {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IOpcServer CreateMockOpcServer() {
            return new MockOpcServer();
        }
    }
}
