using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL {

    public class CurrentDoneCycleCountStatus : PlcAddress {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public CurrentDoneCycleCountStatus(string address)
            : base(address) {

            }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Read() {
            if (Config.IsMock) {
                return ReadMock();
            } else {
                return ReadFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int ReadMock() {
            //todo:
            //
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int ReadFact() {
            var obj = ReadFromOpc();
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damValue"></param>
        public void Write(int doneCycleCount) {
            if (Config.IsMock) {
                WriteMock(doneCycleCount);
            } else {
                WriteFact(doneCycleCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteFact(int doneCycleCount) {
            WriteToOpc(doneCycleCount);
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteMock(int damValue) {
            //todo:
            //
        }
    }
}
