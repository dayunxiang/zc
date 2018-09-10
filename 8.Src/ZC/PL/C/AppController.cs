using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;
using PL.Hardware;

namespace PL
{

    public class AppController
    {
        #region Members
        static private Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _isChecking = false;
        private PlController _plController = null;
        private Timer _timer;
        private App _app;
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        public AppController(App app, Address2 address2)
        {
            this._app = app;
            this.ControllerStatus = new AppControllerStatus(address2.AppControlStatus, ControllerStatusEnum.Idle);
            this.AutoManualStatus = new AutoManualStatus(address2.AutoManual);
            this.ZtPlcStatus = new ZtPlcStatus(address2.ZtPlcStatus);
            this.PlOptionsReader = new PlOptionsReader();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        static private void D(string msg)
        {
            _logger.Debug(msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        static private void D(string msg, params object[] args)
        {
            _logger.Debug(msg, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsChecking()
        {
            return _isChecking;
        }

        /// <summary>
        /// call it when app exit
        /// </summary>
        public void Close()
        {
            this.ControllerStatus.Value = ControllerStatusEnum.NotRun;
        }

        /// <summary>
        /// 
        /// </summary>
        public AutoManualStatus AutoManualStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public AppControllerStatus ControllerStatus
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public PlOptionsReader PlOptionsReader
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
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
            if (!_isChecking)
            {
                _isChecking = true;
                OnCheck();
                _isChecking = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string[] GetSubscriptionItemNames()
        {
            var r = new List<string>(2000);
            foreach (var dam in _app.Dams.ToArray())
            {
                foreach(var gun in dam.Guns.ToArray())
                {
                    r.Add(gun.Switch.Address);
                    r.Add(gun.Fault.Address);
                    r.Add(gun.Remote.Address);
                    r.Add(gun.Mark.Address);
                }
            }
            var a2 = Address2.Instance;
            r.Add(a2.AppControlStatus);
            r.Add(a2.AutoManual);
            r.Add(a2.CycleCount);
            r.Add(a2.CycleMode);
            r.Add(a2.GunCountPerGroup);
            r.Add(a2.PlTimeSecond);
            r.Add(a2.WorkDam);
            r.Add(a2.ZtPlcStatus);
            return r.ToArray();

            //var guns = _app.Dams.First.Value.Guns.ToArray();
            //foreach( var gun in guns)
            //{
            //    r.Add(gun.Fault.Address);
            //    r.Add(gun.Remote.Address);
            //    r.Add(gun.Mark.Address);
            //}

            //r.Add("[a]Global_Control.AutoManual");
            //r.Add("[a]Global_Control.ztplcstatus");
            //r.Add("[a]Global_Control.AppControlStatus");
            //r.Add("[a]Global_Control.CycleCount");
            //r.Add("[a]Global_Control.PLtimeSecond");
            //r.Add("[a]Global_Control.CycMode");
            //r.Add("[a]Global_Control.WorkDam");
            //r.Add("[a]Global_Control.GunCountPerGroup");
            //return r.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnCheck()
        {
            Lm.D("OnCheck()");

            if (!_app.Opc.IsConnected())
            {
                var subscriptionItemNames = GetSubscriptionItemNames();
                bool success = _app.Opc.Connect();
                if(success)
                {
                    var itemNames = GetSubscriptionItemNames();
                    _app.Opc.AddSubscriptionItems(itemNames);

                    this.ControllerStatus.Value = ControllerStatusEnum.Idle;
                    this.ControllerStatus.Write();
                }
                return;
            }

            if (AutoManualStatus.Read() == AutoManualStatusEnum.Auto)
            {
                var controllerStatusEnum = this.ControllerStatus.Value;
                var ztPlcStatusEnum = ZtPlcStatus.Read();
                if (ztPlcStatusEnum == ZtPlcStatusEnum.Start)
                {
                    #region start
                    if (controllerStatusEnum == ControllerStatusEnum.Idle ||
                        controllerStatusEnum == ControllerStatusEnum.Completed)
                    {
                        this.ControllerStatus.Value = ControllerStatusEnum.Working;
                        var options = this.PlOptionsReader.Read();
                        _plController = new PlController(options);
                        _plController.Start();
                    }
                    else if (controllerStatusEnum == ControllerStatusEnum.Working)
                    {
                        var checkResult = _plController.Check();
                        if (checkResult == PlCheckResult.Completed)
                        {
                            this.ControllerStatus.Value = ControllerStatusEnum.Completed;
                            this.ZtPlcStatus.Write(ZtPlcStatusEnum.Completed);
                            _plController.Close();
                            _plController = null;
                        }
                        else
                        {
                            // working
                            //
                        }
                    }
                    else
                    {
                        // NotRun ?
                        D("unknown controller status: {0}", controllerStatusEnum);
                    }
                    #endregion start
                }
                else if (ztPlcStatusEnum == ZtPlcStatusEnum.Stop)
                {
                    #region stop
                    if (controllerStatusEnum == ControllerStatusEnum.Idle ||
                     controllerStatusEnum == ControllerStatusEnum.Completed)
                    {
                        //nothing
                        //
                    }
                    else if (controllerStatusEnum == ControllerStatusEnum.Working)
                    {
                        Debug.Assert(_plController != null);

                        _plController.Stop();
                        this.ControllerStatus.Value = ControllerStatusEnum.Completed;
                        this.ZtPlcStatus.Write(ZtPlcStatusEnum.Completed);

                    }
                    else
                    {
                        D("unknown controller status: {0}", controllerStatusEnum);
                    }
                    #endregion stop
                }
                else if (ztPlcStatusEnum == ZtPlcStatusEnum.Completed)
                {
                    // nothind
                    //
                }
                else
                {
                    D("unknown ztPlcStatus: {0}", ztPlcStatusEnum);
                }
            }
            else
            {
                // manual, nothing
                //
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = Config.CheckInterval;
                _timer.Tick += _timer_Tick;
                _timer.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e)
        {
            Check();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsStarted()
        {
            return _timer != null && _timer.Enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
            }
        }
    }
}
