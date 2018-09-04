using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class ControllerStatus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initValue"></param>
        public ControllerStatus(ControllerStatusEnum initValue)
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
            throw new NotImplementedException();
        } 
    }

    public enum ControllerStatusEnum
    {
        /// <summary>
        /// 
        /// </summary>
        NotRun = 0,
        /// <summary>
        /// 
        /// </summary>
        Idle = 1,
        /// <summary>
        /// 
        /// </summary>
        Working = 2,
        /// <summary>
        /// 
        /// </summary>
        Completed = 3,
    }

    public class AutoManualStatus
    {
        public AutoManualStatusEnum Read()
        {
            throw new NotImplementedException();
        }
    }

    public enum AutoManualStatusEnum
    {
        Manual = 0,
        Auto = 1,
    }

    public class ZtPlcStatus
    {
        public ZtPlcStatus Read()
        {
            throw new NotImplementedException();
        }
    }

    public class PlOptions
    {
        public int CycleTimes { get; set; }
        public TimeSpan PlTimeSpan { get; set; }
        public CycleMode CycleMode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// valid when CycleMode is SelectedDam
        /// bit 0 - dam 0 selected
        /// bit 1 - dam 1 selected
        /// ...
        /// </remarks>
        public int WorkDam { get; set; }

        public int GunCountPerGroup { get; set; }
    }

    public class PlOptionsReader
    {
        public PlOptions Read()
        {
            throw new NotImplementedException();
        }
    }

    public enum ZtPlStatusEnum
    {
        Stop = 0,
        Start = 1,
        Completed = 2,
    }

    public enum CycleMode
    {
        AllDam = 0,
        SelectedDam = 1,
    }

    public enum GunStatus
    {
        Close = 0,
        Open = 1,
    }

    public enum FaultStatus
    {
        Normal = 0,
        Fault = 1,
    }
    public enum RemoteStatus
    {
        Local = 0,
        Remote = 1
    }
    public enum MarkStatus
    {
        Enabled = 0,
        Disabled = 1,
    }


    public class Controller
    {
        private bool _isChecking = false;

        /// <summary>
        /// 
        /// </summary>
        public Controller()
        {
            this.ControllerStatus = new ControllerStatus(ControllerStatusEnum.Idle);
            this.AutoManualStatus = new AutoManualStatus();
            this.ZtPlcStatus = new ZtPlcStatus();
            this.PlOptionsReader = new PlOptionsReader();
        }

        /// <summary>
        /// 
        /// </summary>
        public AutoManualStatus AutoManualStatus
        {
            get;
            private set;
        }

        public ControllerStatus ControllerStatus
        {
            get;
            private set; 
        }

        public PlOptionsReader PlOptionsReader
        {
            get;
            private set;
        }

        public ZtPlcStatus ZtPlcStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Check()
        {
            if(!_isChecking)
            {
                _isChecking = true;
                OnCheck();
                _isChecking = false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void OnCheck()
        {

        }
    }

    public class App
    {
        public App()
        {

        }

        public Controller Controller { get; private set; }
        public DamList Dams { get; private set; }
    }
}
