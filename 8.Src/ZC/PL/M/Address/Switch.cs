using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PLC;

namespace PL {
    public class Switch : PlcAddress {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Switch(string address)
            : base(address) {
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpened {
            get {
                    return IsOpenedFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open() {
                OpenFact();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close() {
                CloseFact();
        }

        #region fact
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsOpenedFact() {
            var val = ReadFromOpc();
            var n = Convert.ToInt32(val);
            return n == (int)SwitchStatusEnum.Open;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenFact() {
            WriteToOpc((int)SwitchStatusEnum.Open);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseFact() {
            WriteToOpc((int)SwitchStatusEnum.Close);
        }
        #endregion //fact
    }
}
