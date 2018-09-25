using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    /// <summary>
    /// 
    /// </summary>
    public class CurrentWorkingDamStatus : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public CurrentWorkingDamStatus(string address)
            : base(address)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Read()
        {
            if (Config.IsMock)
            {
                return ReadMock();
            }
            else
            {
                return ReadFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int ReadMock()
        {
            //todo:
            //
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int ReadFact()
        {
            var obj = ReadFromOpc();
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damValue"></param>
        public void Write(int damValue)
        {
            if (Config.IsMock)
            {
                WriteMock(damValue);
            }
            else
            {
                WriteFact(damValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteFact(int damValue)
        {
            WriteToOpc(damValue);
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteMock(int damValue)
        {
            //todo:
            //
        }
    }
}
