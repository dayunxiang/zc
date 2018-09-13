using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Pump : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Pump(string address)
            :base(address)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Run()
        {
            RunFact();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            StopFact();
        }

        /// <summary>
        /// 
        /// </summary>
        private void StopFact()
        {
            this.WriteToOpc((int)PumpStopEnum.Stop);
        }

        /// <summary>
        /// 
        /// </summary>
        private void RunFact()
        {
            this.WriteToOpc((int)PumpStopEnum.Run);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsRun()
        {
            return IsRunFact();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsStop()
        {
            return IsStopFact();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsStopFact()
        {
            var v = ReadFromOpc();
            var n = Convert.ToInt32(v);
            return n == (int)PumpStopEnum.Stop;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsRunFact()
        {
            var v = ReadFromOpc();
            var n = Convert.ToInt32(v);
            return n == (int)PumpStopEnum.Run;
        }
    }
}
