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

    public class CellColorTimer {

        static private Dictionary<Color, DataGridViewCellStyle> _dict = new Dictionary<Color, DataGridViewCellStyle>();
        public DataGridViewCellStyle GetStyle(Color color, DataGridViewCellStyle style) {
            if (_dict.ContainsKey(color)) {
                return _dict[color];
            } else {
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
                Interval = 300,
                Enabled = true,
            };
            _timer.Tick += _timer_Tick;
        }

        private void _timer_Tick(object sender, EventArgs e) {
            //_cell.Style.BackColor = Color.White;
            _cell.Style = GetStyle(Color.White, _cell.Style);
            _timer.Enabled = false;
            _timer.Dispose();
        }
    }
}
