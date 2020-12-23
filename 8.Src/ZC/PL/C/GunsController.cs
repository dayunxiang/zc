using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;
using PL.Hardware;

namespace PL {
    public class GunsController {
        #region Members
        private PlController _plController;
        private WorkGunGroup _workingGunGroup;
        private PlOptions _plOptions;
        private DateTime _openDateTime;
        private DateTime _closeDateTime;
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guns"></param>
        public GunsController(PlController plController, WorkGunGroup workGunGroup, PlOptions plOptions) {
            if (plController == null) {
                throw new ArgumentNullException("plController");
            }
            if (workGunGroup== null) {
                throw new ArgumentNullException("workGunGroup");
            }
            if (plOptions == null) {
                throw new ArgumentNullException("plOptions");
            }

            if (workGunGroup.WorkGuns.Count == 0) {
                throw new ArgumentException("workGunGroup guns count == 0");
            }

            _plController = plController;
            _workingGunGroup = workGunGroup;
            _plOptions = plOptions;
        }

        #region GetNextWorkGunGroup
        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPassTail"></param>
        /// <returns></returns>
        public WorkGunGroup GetNextWorkGunGroup(out bool isPassTail) {
            isPassTail = false;

            var tailGun = GetTailGun();
            if (_workingGunGroup.IsIncludeGun(tailGun)) {
                isPassTail = true;
            }

            // var lastGun = _workingGunGroup.GetLastGun();
            var lastGun = _workingGunGroup.WorkGuns.GetLast();
            int count = _plOptions.GunCountPerGroup;

            WorkGunGroup wgg = new WorkGunGroup();
            while (count > 0) {
                var gun = GetNextGun(lastGun);

                // searched to working gun
                //
                if (_workingGunGroup.WorkGuns.Contains(gun)) {
                    break;
                }

                if (gun.CanUse(App.GetApp().MaterialAreas)) {
                    wgg.WorkGuns.Add(gun);
                    count--;
                }
                wgg.SearchedGuns.Add(gun);
                lastGun = gun;
            }
            return wgg;
        }
        #endregion //GetNextWorkGunGroup

        /// <summary>
        /// get last gun for work dams
        /// </summary>
        /// <returns></returns>
        private Gun GetTailGun() {
            DamList workDams = _plController.App.Dams.GetWorkDams(_plOptions);
            if (workDams.Count == 0) {
                throw new InvalidOperationException("dams count == 0");
            }
            var lastDam = workDams[workDams.Count - 1];
            var lastGun = lastDam.Guns.Last.Value;
            return lastGun;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        private Gun GetNextGun(Gun current) {
            var next = current.Next;
            if (next == null) {
                var dam = GetNextDam(current.Dam);
                return dam.Guns.First.Value;
            } else {
                return next;
            }
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //private Dam GetNextGunDam() {
        //    var lastGun = _workingGunGroup.GetLastGun();
        //    var currentDam = lastGun.Dam;
        //    return GetNextDam(currentDam);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextDam"></param>
        private Dam GetNextDam(Dam dam) {
            var nextDam = dam.GetNextDam();
            if (nextDam == null) {
                // get first dam
                var ownerDams = dam.GetOwnerDamList();
                nextDam = ownerDams.First.Value;
            }

            if (_plOptions.IsWorkDam(nextDam)) {
                return nextDam;
            } else {
                return GetNextDam(nextDam);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Open() {
            foreach (var gun in _workingGunGroup.WorkGuns) {
                gun.Switch.Open();
            }

            // set current working dam
            //
            var damValue = _workingGunGroup.GetWorkingDamValue();
            GetCurrentWorkingDamStatus().Write(damValue);

            this._openDateTime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CurrentWorkingDamStatus GetCurrentWorkingDamStatus() {
            //return App.GetApp().AppController.CurrentWorkingDamStatus;
            return _plController.GetCurrentWorkingDamStatus();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Close() {
            foreach (var gun in _workingGunGroup.WorkGuns) {
                gun.Switch.Close();
            }

            // reset gun work status
            //
            foreach (var gun in _workingGunGroup.SearchedGuns) {
                //gun.GunWorkStatus.Status = GunWorkStatusEnum.Normal;
            }

            this._closeDateTime = DateTime.Now;
        }

        #region Check
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GunsCheckResultEnum Check() {
            WriteRemainingTime();

            bool isTimeOut = _plOptions.IsTimeout(_openDateTime);
            if (isTimeOut) {
                return GunsCheckResultEnum.Timeout;
            } else {
                ProcessWorkingGuns();
                return GunsCheckResultEnum.Working;
            }
        }
        #endregion //Check

        #region ProcessWorkingGuns
        /// <summary>
        /// 
        /// </summary>
        private void ProcessWorkingGuns() {
            var workingGunsCopy = this._workingGunGroup.WorkGuns.ToList();
            foreach (var workingGun in workingGunsCopy) {
                if (IsGunNeedClose(workingGun)) {
                    ProcessNeedCloseGun(workingGun);
                }
            }
        }
        #endregion //ProcessWorkingGuns

        #region ProcessNeedCloseGun
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workingGun"></param>
        private void ProcessNeedCloseGun(Gun workingGun) {
            //workingGun.GunWorkStatus.Status = GunWorkStatusEnum.NotWorkWithCart;

            Gun last = _workingGunGroup.GetLastGun();
            Gun nextGun = GetNextGun(last);

            while (nextGun != null) {
                _workingGunGroup.SearchedGuns.Add(nextGun);

                if (!nextGun.CanUse(App.GetApp().MaterialAreas)) {
                    nextGun = GetNextGun(nextGun);
                } else {
                    _workingGunGroup.WorkGuns.Add(nextGun);
                    nextGun.Switch.Open();

                    // remove and close fault gun
                    _workingGunGroup.WorkGuns.Remove(workingGun);
                    workingGun.Switch.Close();
                    break;
                }
            }
        }
        #endregion //ProcessNeedCloseGun

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workingGun"></param>
        /// <returns></returns>
        private static bool IsGunNeedClose(Gun workingGun) {
            return
                workingGun.Fault.IsFault ||
                workingGun.IsCoverCart();
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteRemainingTime() {
            var remaining = this.RemainingTimeWithSecond;
            //PlTimeRemaining.Instance.Write(remaining);
            var plTimeRemaining = _plController.App.PlTimeRemaining;
            plTimeRemaining.Write(remaining);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //internal void ToNextGroup()
        //{
        //    // back current guns
        //    _previousGuns = _guns;

        //    var nextGuns = GetNextGuns();
        //    _guns = nextGuns;

        //    Open();
        //}
        public DateTime DiscardDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanClose(int discardDelaySecond) {
            var tsDelay = TimeSpan.FromSeconds(discardDelaySecond);

            var ts = DateTime.Now - this.DiscardDateTime;

            if (ts < TimeSpan.Zero || ts >= tsDelay) {
                return true;
            } else {
                return false;
            }
        }

        #region RunningTimeWithSecond
        /// <summary>
        /// 
        /// </summary>
        public int RunningTimeWithSecond {
            get {
                var ts = DateTime.Now - _openDateTime;
                return (int)ts.TotalSeconds;
            }
        }
        #endregion //RunningTimeWithSecond

        #region RemainingTimeWithSecond
        /// <summary>
        /// 
        /// </summary>
        public int RemainingTimeWithSecond {
            get {
                var plTime = (int)this._plOptions.PlTimeSpan.TotalSeconds;
                var remainingTime = plTime - RunningTimeWithSecond;
                if (remainingTime < 0) {
                    remainingTime = 0;
                }
                return remainingTime;
            }
        }
        #endregion //RemainingTimeWithSecond
    }
}
