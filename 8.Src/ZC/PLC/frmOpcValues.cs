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
    public partial class frmOpcValues : Form {

        static public frmOpcValues Instance = new frmOpcValues();

        public frmOpcValues() {
            InitializeComponent();
        }

        public Items Items {
            get;
            set;
        }

        public void BindItemsToDgv() {
            this.dgvOpcValues.DataSource = this.Items;
        }

        public object Read(string name) {
            foreach (DataGridViewRow row in this.dgvOpcValues.Rows) {
                if (((string)row.Cells[0].Value).Equals(name)) {
                    var cell = row.Cells[1];
                    //cell.Style.BackColor = Color.Green;
                    new CellColorTimer(cell, Color.Green);
                    return row.Cells[1].Value;
                }
            }

            throw new Exception("not find name: " + name);
        }

        internal void Write(string name, object value) {
            foreach (DataGridViewRow row in this.dgvOpcValues.Rows) {
                if (((string)row.Cells[0].Value).Equals(name)) {
                    var cell = row.Cells[1];
                    cell.Value = (int)value;
                    //cell.Style.BackColor = Color.Green;
                    new CellColorTimer(cell, Color.Red);
                }
            }
        }
    }

    public class CellColorTimer {

        static private Dictionary<Color, DataGridViewCellStyle> _dict = new Dictionary<Color, DataGridViewCellStyle>();
        public DataGridViewCellStyle GetStyle(Color color, DataGridViewCellStyle style) {
            if (_dict.ContainsKey(color)) {
                return _dict[color];
            }
            else {
                var s2 = style.Clone();
                s2.BackColor = color;
                _dict[color] = s2;
                return s2;
            }
        }

        private DataGridViewCell _cell;
        private Timer _timer;
        public CellColorTimer(DataGridViewCell cell, Color color) {
            _cell = cell;
            //_cell.Style.BackColor = color;
            _cell.Style = GetStyle(color, _cell.Style);

            _timer = new Timer() {
                Interval = 200,
                Enabled = true,
            };
            _timer.Tick += _timer_Tick;
        }

        void _timer_Tick(object sender, EventArgs e) {
            //_cell.Style.BackColor = Color.White;
            _cell.Style = GetStyle(Color.White, _cell.Style);
            _timer.Enabled = false;
            _timer.Dispose();
        }

    }

    public class Item {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    public class Items : List<Item> {

        public void SetGcValues() {
            Set("[a]Global_Control.AutoManual", 1);
            //"ZtPlcStatus" : "[a]Global_Control.ztplcstatus",
            //"AppControlStatus" : "[a]Global_Control.AppControlStatus",
            Set("[a]Global_Control.CycleCount", 2);
            Set("[a]Global_Control.PLtimeSecond", 30);
            //"CycleMode" : "[a]Global_Control.CycMode",
            //"WorkDam" : "[a]Global_Control.WorkDam",
            Set("[a]Global_Control.GunCountPerGroup", 6);
            //"PlTimeRemaining" : "[a]Global_Control.PLtimeremaining",
            //"CycleEndStopPump" : "[a]Global_Control.CycEndStoppump",
            //"CurrentWorkingDam" : "[a]Global_Control.CurrentWorkingDam",
            //"CurrentDoneCycleCount" : "[a]Global_Control.CurrentDoneCycleCount"

            Set("StockGPS[0].Stock_Ground_IDString", "2A");
            Set("StockGPS[1].Stock_Ground_IDString", "2B");
            Set("StockGPS[2].Stock_Ground_IDString", "2C");
            Set("StockGPS[3].Stock_Ground_IDString", "2D");
            Set("StockGPS[4].Stock_Ground_IDString", "2E");
            Set("StockGPS[5].Stock_Ground_IDString", "2F");
            Set("StockGPS[6].Stock_Ground_IDString", "2G");
            Set("StockGPS[7].Stock_Ground_IDString", "2H");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="n"></param>
        private void Set(string name, object n) {
            foreach (var item in this) {
                if (StringComparer.OrdinalIgnoreCase.Equals(item.Name, name)) {
                    item.Value = n;
                }
            }
        }
    }


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
