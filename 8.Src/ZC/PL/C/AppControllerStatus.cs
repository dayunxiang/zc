using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class AppControllerStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initValue"></param>
        public AppControllerStatus(ControllerStatusEnum initValue)
        {
            _value = initValue;
            Write();
        }

        /// <summary>
        /// 
        /// </summary>
        public ControllerStatusEnum Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    Write();
                }
            }
        } private ControllerStatusEnum _value;

        /// <summary>
        /// 
        /// </summary>
        private void Write()
        {
            // todo: write _value to plc
            //
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsWorking()
        {
            return this.Value == ControllerStatusEnum.Working;
        }
    }
}
