using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;

namespace PL {

    public class PlController {
        /// <summary>
        /// 
        /// </summary>
        private enum PlControllerStatusEnum {
            Init = 0,
            Working = 1,
            StopPump = 2,
            //End = 3,
        }

        #region Members
        static private Logger _logger = LogManager.GetCurrentClassLogger();

        //private bool _isWorking;
        private AppController _appController;
        private PlControllerStatusEnum _plControllerStatus;
        private DateTime _beginDateTime;
        private DateTime _endDateTime;
        private DateTime _stopPumpDateTime;
        private GunsController _discardGunsController;
        private GunsController _workingGunsController;
        private int _cycleCount = 0;
        #endregion //Members

        #region PlController
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public PlController(AppController appController, PlOptions options) {
            this._appController = appController;
            this.PlOptions = options;
            _plControllerStatus = PlControllerStatusEnum.Init;
        }
        #endregion //PlController

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        static private void Debug(string msg) {
            _logger.Debug(msg);
        }

        #region IsInitStatus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsInitStatus() {
            return _plControllerStatus == PlControllerStatusEnum.Init;
        }
        #endregion //IsInitStatus

        #region CycleCountChanged
        /// <summary>
        /// 
        /// </summary>
        private void CycleCountChanged() {
            var doneCycleCountStatus = GetCurrentDoneCycleCountStatus();
            doneCycleCountStatus.Write(_cycleCount);
        }
        #endregion //CycleCountChanged


        #region IsWorkingStatus
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsWorkingStatus() {
            return _plControllerStatus == PlControllerStatusEnum.Working;
            // || _plControllerStatus == PlControllerStatus.StopPump;
        }
        #endregion //IsWorkingStatus

        #region PlOptions
        /// <summary>
        /// 
        /// </summary>
        public PlOptions PlOptions {
            get;
            private set;
        }
        #endregion //PlOptions

        #region Start
        /// <summary>
        /// 
        /// </summary>
        public void Start() {
            //if (!IsWorkingStatus())
            if (IsInitStatus()) {
                _plControllerStatus = PlControllerStatusEnum.Working;
                _beginDateTime = DateTime.Now;
                // 1. get guns -> working guns
                // 2. guns open
                var gunsController = GetGunsController();
                gunsController.Open();
                _cycleCount = 1;
                CycleCountChanged();
            }
        }
        #endregion //Start

        #region Check
        /// <summary>
        /// 
        /// </summary>
        internal PlCheckResultEnum Check() {
            if (IsStopPumpStatus()) {
                return CheckStopPump();
            } else if (IsWorkingStatus()) {
                return CheckWorking();
            } else {
                throw new InvalidOperationException("pl controller status invalid");
            }
        }
        #endregion //Check

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsStopPumpTimeOut() {
            var ts = DateTime.Now - _stopPumpDateTime;
            if (ts < TimeSpan.Zero || ts.TotalSeconds >= Config.GunsCloseDelaySecondWhenStopPump) {
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsStopPumpStatus() {
            return _plControllerStatus == PlControllerStatusEnum.StopPump;
        }

        #region CheckStopPump
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlCheckResultEnum CheckStopPump() {
            if (IsStopPumpTimeOut()) {
                _workingGunsController.Close();
                _workingGunsController = null;
                GetCurrentWorkingDamStatus().Write(0);
                return PlCheckResultEnum.Completed;
            } else {
                return PlCheckResultEnum.Working;
            }
        }
        #endregion //CheckStopPump

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public App App {
            get {
                return this._appController.App;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CurrentWorkingDamStatus GetCurrentWorkingDamStatus() {
            //return App.GetApp().AppController.CurrentWorkingDamStatus;
            return _appController.CurrentWorkingDamStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CurrentDoneCycleCountStatus GetCurrentDoneCycleCountStatus() {
            //return App.GetApp().AppController.CurrentDoneCycleCountStatus;
            return _appController.CurrentDoneCycleCountStatus;
        }


        #region RefreshCartLocation
        /// <summary>
        /// 
        /// </summary>
        private void RefreshCartLocation() {
            var carts = _appController.App.Carts;
            carts.RefreshLocations();
        }
        #endregion //RefreshCartLocation

        #region CheckWorking
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private PlCheckResultEnum CheckWorking() {
            //-1. refresh cart location
            //
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

            RefreshCartLocation();

            CheckDiscardGuns();


            var gunsController = GetGunsController();

            var gunsCheckResult = gunsController.Check();

            if (gunsCheckResult == GunsCheckResultEnum.Timeout) {
                //todo: check cycle count 
                //
                bool isPassTail;
                var nextGuns = gunsController.GetNextWorkGunGroup(out isPassTail);
                if (isPassTail) {
                    this._cycleCount += 1;
                    CycleCountChanged();

                    if (_cycleCount > this.PlOptions.CycleTimes) {
                        //gunsController.Close();
                        //return PlCheckResult.Completed;
                        StopPump();
                        this._plControllerStatus = PlControllerStatusEnum.StopPump;

                        // clear cycle count
                        //
                        this._cycleCount = 0;
                        CycleCountChanged();

                        return PlCheckResultEnum.Working;
                    }
                }

                // if discardGunsController not null, need close discard guns
                //
                if (_discardGunsController != null) {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }

                _discardGunsController = gunsController;
                _discardGunsController.DiscardDateTime = DateTime.Now;

                var nextGunsController = new GunsController(this, nextGuns, this.PlOptions);
                _workingGunsController = nextGunsController;
                nextGunsController.Open();

                return PlCheckResultEnum.Working;
            } else if (gunsCheckResult == GunsCheckResultEnum.Working) {
                return PlCheckResultEnum.Working;
            } else {
                Debug("unknown gunsCheckResult: " + gunsCheckResult);
                return PlCheckResultEnum.Working;
            }
        }
        #endregion //CheckWorking

        #region CheckDiscardGuns
        /// <summary>
        /// 
        /// </summary>
        private void CheckDiscardGuns() {
            if (_discardGunsController != null) {
                var canClose = _discardGunsController.CanClose(Config.DiscardGunsCloseDelay);
                if (canClose) {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }
            }
        }
        #endregion //CheckDiscardGuns

        #region StopPump
        /// <summary>
        /// 
        /// </summary>
        private void StopPump() {
            //Pump.Instance.Stop();
            var pump = _appController.App.Pump;
            pump.Stop();
            this._stopPumpDateTime = DateTime.Now;
        }
        #endregion //StopPump

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GunsController GetGunsController() {
            if (_workingGunsController == null) {
                //WorkGunGroup guns = App.GetApp().Dams.GetFirstGuns(this.PlOptions);
                var dams = _appController.App.Dams;
                // todo: guns count may be 0, when dam material heap can not wet
                //
                WorkGunGroup guns = dams.GetFirstGuns(this.PlOptions);
                _workingGunsController = new GunsController(this, guns, this.PlOptions);
            }
            return _workingGunsController;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Close() {
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Stop() {
            // todo:
            //
            if (IsWorkingStatus()) {
                if (_discardGunsController != null) {
                    _discardGunsController.Close();
                    _discardGunsController = null;
                }
                _workingGunsController.Close();
                _workingGunsController = null;
            }
        }
    }
}
