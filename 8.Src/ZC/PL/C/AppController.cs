using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;

namespace PL
{

    public class AppController
    {
        #region Members
        static private Logger _logger = LogManager.GetCurrentClassLogger();
        private bool _isChecking = false;
        private PlController _plController = null;
        private Timer _timer;
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        public AppController()
        {
            this.ControllerStatus = new AppControllerStatus(ControllerStatusEnum.Idle);
            this.AutoManualStatus = new AutoManualStatus();
            this.ZtPlcStatus = new ZtPlcStatus();
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
        private void OnCheck()
        {
            Lm.D("OnCheck()");

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
