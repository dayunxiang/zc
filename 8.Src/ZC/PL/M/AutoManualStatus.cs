using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class AutoManualStatus : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public AutoManualStatus(string address)
            : base(address)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AutoManualStatusEnum Read()
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
        private AutoManualStatusEnum ReadMock()
        {
            return AutoManualUI.Checked ?
                AutoManualStatusEnum.Auto : AutoManualStatusEnum.Manual;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private AutoManualStatusEnum ReadFact()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        public CheckBox AutoManualUI
        {
            get;
            set;
        }
    }
}
