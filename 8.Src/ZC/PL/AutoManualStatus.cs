using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class AutoManualStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AutoManualStatusEnum Read()
        {
            //todo:
            return AutoManualUI.Checked ? 
                AutoManualStatusEnum.Auto : AutoManualStatusEnum.Manual;
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
