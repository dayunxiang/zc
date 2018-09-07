using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class AppControllerStatus : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initValue"></param>
        public AppControllerStatus(string address, ControllerStatusEnum initValue)
            : base(address)
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
