using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC {

    public interface IOpcServer {
        bool Connect();
        bool IsConnected();
        void Disconnect();

        object Read(string itemName);
        object[] Read(string[] itemNames);
        void Write(string itemName, object value);

        void AddSubscriptionItems(string[] itemNames);
    }
}
