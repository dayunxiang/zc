using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PLC {

    /// <summary>
    /// 
    /// </summary>
    public class MockOpcServer : PLC.IOpcServer {

        public MockOpcServer() {
        }

        public bool Connect() {
            return true;
        }

        public bool IsConnected() {
            return true;
        }

        public void Disconnect() {
        }

        public object Read(string itemName) {
            return frmOpcValues.Instance.Read(itemName);
        }

        public void Write(string itemName, object value) {
            //Console.WriteLine("write " + itemName + value);
            frmOpcValues.Instance.Write(itemName, value);
        }


        public object[] Read(string[] itemNames) {
            var r = new List<object>();
            foreach (var name in itemNames) {
                r.Add(this.Read(name));
            }
            return r.ToArray();
        }

        public void AddSubscriptionItems(string[] itemNames) {
            //throw new NotImplementedException();

            var items = new Items();
            foreach (var n in itemNames) {
                items.Add(new Item() { Name = n, Value = 0 });
            }

            items.SetGcValues();
            frmOpcValues.Instance.Items = items;
            frmOpcValues.Instance.BindItemsToDgv();
            frmOpcValues.Instance.Show();
        }
    }
}
