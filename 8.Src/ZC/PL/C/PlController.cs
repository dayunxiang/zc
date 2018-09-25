using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;

namespace PL
{

    public class PlController
    {

        private enum PlControllerStatus
        {
            Init = 0,
            Working = 1,
            StopPump = 2,
            //End = 3,
        }

        #region Members
        static private Logger _logger = LogManager.GetCurrentClassLogger();

        //private bool _isWorking;
        private PlControllerStatus _plControllerStatus;
        private DateTime _beginDt;
        private DateTime _endDt;
        private DateTime _stopPumpDt;
        private GunsController _discardGunsController;
        private GunsController _gunsController;
        private int _cycleCount = 0;
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PlController(PlOptions options)
        {
            this.PlOptions = options;
            _plControllerStatus = PlControllerStatus.Init;
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
        /// <returns></returns>
        public bool IsInitStatus()
        {
            return _plControllerStatus == PlControllerStatus.Init;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsWorkingStatus()
        {
            return _plControllerStatus == PlControllerStatus.Working;
                // || _plControllerStatus == PlControllerStatus.StopPump;
        }

        /// <summary>
        /// 
        /// </summary>
        public PlOptions PlOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            //if (!IsWorkingStatus())
            if (IsInitStatus())
            {
                _plControllerStatus = PlControllerStatus.Working;
                _beginDt = DateTime.Now;
                // 1. get guns -> working guns
                // 2. guns open
                var gunsController = GetGunsController();
                gunsController.Open();
                _cycleCount = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal PlCheckResult Check()
        {
            if (IsStopPumpStatus())
            {
                return CheckStopPump();
            }
            else if (IsWorkingStatus())
            {
                return CheckWorking();
            }
            else
            {
                throw new InvalidOperationException("pl controller status invalid");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsStopPumpTimeOut()
        {
            var ts = DateTime .Now - _stopPumpDt;
            if(ts < TimeSpan.Zero || ts .TotalSeconds >= Config.GunsCloseDelaySecondWhenStopPump)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsStopPumpStatus()
        {
            return _plControllerStatus == PlControllerStatus.StopPump;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlCheckResult CheckStopPump()
        {
            if (IsStopPumpTimeOut())
            {
                _gunsController.Close();
                _gunsController = null;
                GetCurrentWorkingDamStatus().Write(0);
                return PlCheckResult.Completed;
            }
            else
            {
                return PlCheckResult.Working;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CurrentWorkingDamStatus GetCurrentWorkingDamStatus()
        {
            return App.GetApp().AppController.CurrentWorkingDamStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlCheckResult CheckWorking()
        {
            // 0. discard guns controller close
            //
            // 1. working guns timeout
            //    y - cycle count <= options.cycle count
            //        y - get next guns : (guns, is last gun)
            //            cycle count + is last gun
            //            next guns open
            //            wait 5 second
            //            current guns close
            //        n - work completed
            //
            //    n - return
            if (_discardGunsController != null)
            {
                var canClose = _discardGunsController.CanClose(Config.DiscardGunsCloseDelay);
                if (canClose)
                {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }
            }

            var gunsController = GetGunsController();

            var gunsCr = gunsController.Check();

            if (gunsCr == GunsCheckResult.Timeout)
            {
                //todo: check cycle count 
                //
                bool isPassTail;
                var nextGuns = gunsController.GetNextWorkGunGroup(out isPassTail);
                if (isPassTail)
                {
                    this._cycleCount += 1;

                    if(_cycleCount > this.PlOptions.CycleTimes)
                    {
                        //gunsController.Close();
                        //return PlCheckResult.Completed;
                        StopPump();
                        this._plControllerStatus = PlControllerStatus.StopPump;

                        return PlCheckResult.Working;
                    }
                }

                // if discardGunsController not null, need close discard guns
                //
                if(_discardGunsController != null)
                {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }

                _discardGunsController = gunsController;
                _discardGunsController.DiscardDt = DateTime.Now;

                var nextGunsController = new GunsController(nextGuns, this.PlOptions);
                _gunsController = nextGunsController;
                nextGunsController.Open();

                return PlCheckResult.Working;
            }
            else if (gunsCr == GunsCheckResult.Working)
            {
                return PlCheckResult.Working;
            }
            else
            {
                D("unknown gunsCheckResult: " + gunsCr);
                return PlCheckResult.Working;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        private void StopPump()
        {
            Pump.Instance.Stop();
            this._stopPumpDt = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GunsController GetGunsController()
        {
            if (_gunsController == null)
            {
                var guns = App.GetApp().Dams.GetFirstGuns(this.PlOptions);
                _gunsController = new GunsController(guns, this.PlOptions);
            }
            return _gunsController;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Close()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Stop()
        {
            // todo:
            //
            if(IsWorkingStatus())
            {
                if(_discardGunsController != null)
                {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }
                _gunsController.Close();
                _gunsController = null;
            }
        }
    }
}
