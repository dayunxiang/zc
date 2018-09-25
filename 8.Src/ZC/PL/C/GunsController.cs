using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;
using PL.Hardware;

namespace PL
{
    public class GunsController
    {
        #region Members
        //private DateTime _discardDt;
        //private GunList _previousGuns;

        private WorkGunGroup _workGunGroup;
        private PlOptions _plOptions;
        private DateTime _openDt;
        private DateTime _closeDt;
        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guns"></param>
        public GunsController(WorkGunGroup workGunGroup, PlOptions plOptions)
        {
            _workGunGroup = workGunGroup;
            _plOptions = plOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isPassTail"></param>
        /// <returns></returns>
        public WorkGunGroup GetNextWorkGunGroup(out bool isPassTail)
        {
            isPassTail = false;

            var tailGun = GetTailGun();
            if(_workGunGroup.IsIncludeGun(tailGun))
            {
                isPassTail = true;
            }

            var lastGun = _workGunGroup.GetLastGun();
            int count = _plOptions.GunCountPerGroup;

            WorkGunGroup wgg = new WorkGunGroup();
            while (count > 0)
            {
                var gun = GetNextGun(lastGun);
                if(gun.CanUse())
                {
                    wgg.WorkGuns.Add(gun);
                    count--;
                }
                wgg.SearchGuns.Add(gun);
                lastGun = gun;
            }
            return wgg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Gun GetTailGun()
        {
            DamList dams = App.GetApp().Dams.GetWorkDams(_plOptions);
            if(dams.Count == 0)
            {
                throw new InvalidOperationException("dams count == 0");
            }
            var lastDam = dams[dams.Count - 1];
            var lastGun = lastDam.Guns.Last.Value;
            return lastGun;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        private Gun GetNextGun(Gun current)
        {
            var next = current.Next;
            if(next == null)
            {
                var dam = GetNextDam(current.Dam);
                return dam.Guns.First.Value;
            }
            else
            {
                return next;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Dam GetNextGunDam()
        {
            var lastGun = _workGunGroup.GetLastGun();
            var currentDam = lastGun.Dam;
            return GetNextDam(currentDam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nextDam"></param>
        private Dam GetNextDam(Dam dam)
        {
            var nextDam = dam.GetNextDam();
            if(nextDam == null)
            {
                // get first dam
                var ownerDams = dam.GetOwnerDamList();
                nextDam = ownerDams.First.Value;
            }

            if(_plOptions.IsWorkDam(nextDam))
            {
                return nextDam;
            }
            else
            {
                return GetNextDam(nextDam);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Open()
        {
            foreach (var gun in _workGunGroup.WorkGuns)
            {
                gun.Switch.Open();
            }

            // set current working dam
            //
            var damValue = _workGunGroup.GetDamValue();
            GetCurrentWorkingDamStatus().Write(damValue);

            this._openDt = DateTime.Now;
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
        internal void Close()
        {
            foreach (var gun in _workGunGroup.WorkGuns)
            {
                gun.Switch.Close();
            }

            this._closeDt = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal GunsCheckResult Check()
        {
            WriteRemainingTime();

            bool isTimeOut = _plOptions.IsTimeout(_openDt);
            if(isTimeOut)
            {
                return GunsCheckResult.Timeout;
            }
            else
            {
                // todo: check fault gun
                // 1. foreach changed workGuns
                //
                var workGunsCopy = this._workGunGroup.WorkGuns.ToList();
                //foreach(var gun in this._workGunGroup.WorkGuns)
                foreach(var gun in workGunsCopy)
                {
                    if(gun.Fault.IsFault)
                    {
                        Gun last = _workGunGroup.GetLastGun();
                        Gun nextGun = GetNextGun(last);

                        while (nextGun != null)
                        {
                            if(!nextGun.CanUse())
                            {
                                _workGunGroup.SearchGuns.Add(nextGun);
                                nextGun = GetNextGun(nextGun);
                            }
                            else
                            {
                                _workGunGroup.WorkGuns.Add(nextGun);
                                _workGunGroup.SearchGuns.Add(nextGun);
                                nextGun.Switch.Open();

                                // remove and close fault gun
                                _workGunGroup.WorkGuns.Remove(gun);
                                gun.Switch.Close();
                                break;
                            }
                        }
                    }
                    else
                    {
                        // nothing
                    }
                }
                return GunsCheckResult.Working;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void WriteRemainingTime()
        {
            var remaining = this.RemainingTimeWithSecond;
            PlTimeRemaining.Instance.Write(remaining);
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
        public DateTime DiscardDt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanClose(int discardDelaySecond)
        {
            var tsDelay = TimeSpan.FromSeconds(discardDelaySecond);

            var ts = DateTime.Now - this.DiscardDt;
            //Lm.D(string.Format("tsDelay: {0}, ts: {1}, DiscardDt: {2}", tsDelay, ts, DiscardDt));

            if (ts < TimeSpan.Zero || ts >= tsDelay)
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
        public int RunningTimeWithSecond
        {
            get
            {
                var ts = DateTime.Now - _openDt;
                return (int)ts.TotalSeconds;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RemainingTimeWithSecond
        {
            get
            {
                var plTime = (int)this._plOptions.PlTimeSpan.TotalSeconds;
                var remainingTime = plTime - RunningTimeWithSecond;
                if (remainingTime < 0)
                {
                    remainingTime = 0;
                }
                return remainingTime;
            }
        }
    }
}
