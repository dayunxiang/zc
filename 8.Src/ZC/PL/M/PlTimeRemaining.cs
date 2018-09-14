using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PLC;

namespace PL
{

    public class PlTimeRemaining : PlcAddress
    {

        /// <summary>
        /// 
        /// </summary>
        static public readonly PlTimeRemaining Instance = new PlTimeRemaining(Hardware.Gc.Instance.PlTimeRemaining);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public PlTimeRemaining(string address)
            : base(address)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remainingTime"></param>
        public void Write(int remainingTime)
        {
            WriteFact(remainingTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="remainingTime"></param>
        private void WriteFact(int remainingTime)
        {
            this.WriteToOpc(remainingTime);
        }
    }
}
