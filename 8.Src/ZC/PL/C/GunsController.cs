using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;

namespace PL
{
    public class GunsController
    {
        #region Members
        //private DateTime _discardDt;
        //private GunList _previousGuns;

        private GunList _guns;
        private PlOptions _plOptions;
        private DateTime _openDt;
        private DateTime _closeDt;

        #endregion //Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guns"></param>
        public GunsController(GunList guns)
        {
            // todo: init options
            _guns = guns;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public GunList GetNextGuns()
        {
            GunList r = new GunList();
            var lastGun = _guns.Last();
            int count = _plOptions.GunCountPerGroup;
            while (count > 0)
            {
                var gun = GetNextGun(lastGun);
                r.Add(gun);
                lastGun = gun;
                count--;
            }
            return r;
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
            var lastGun = _guns.Last();
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
            foreach (var gun in _guns)
            {
                gun.Switch.Open();
            }

            this._openDt = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Close()
        {
            foreach (var gun in _guns)
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
            bool isTimeOut = _plOptions.IsTimeout(_openDt);
            if(isTimeOut)
            {
                return GunsCheckResult.Timeout;
            }
            else
            {
                // todo: check fault gun
                //
                return GunsCheckResult.Working;
            }
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
            if (ts < TimeSpan.Zero || ts >= tsDelay)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }

    public enum GunsCheckResult
    {
        Working = 0,
        Timeout = 1,
    }
}
