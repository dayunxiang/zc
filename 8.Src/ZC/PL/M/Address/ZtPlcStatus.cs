using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class ZtPlcStatus : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public ZtPlcStatus(string address)
            : base(address)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ZtPlcStatusEnum Read()
        {
                return ReadFact();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ZtPlcStatusEnum ReadFact()
        {
            var v = ReadFromOpc();
            var n = Convert.ToInt32(v);
            return (ZtPlcStatusEnum)n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztPlcStatusEnum"></param>
        internal void Write(ZtPlcStatusEnum ztPlcStatusEnum)
        {
                WriteFact(ztPlcStatusEnum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztPlcStatusEnum"></param>
        private void WriteFact(ZtPlcStatusEnum ztPlcStatusEnum)
        {
            WriteToOpc((int)ztPlcStatusEnum);
        }
    }
}
