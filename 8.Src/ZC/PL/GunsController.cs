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
        private GunList _guns;
        private PlOptions _plOptions;

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
        public List<Gun> GetNextGuns()
        {
            List<Gun> r = new List<Gun>();
            var last = _guns.Last();
            int count = _plOptions.GunCountPerGroup;
            while (count > 0)
            {
                var gun = GetNextGun(last);
                r.Add(gun);
                last = gun;
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
        internal void Start()
        {
            foreach (var gun in _guns)
            {
                gun.Switch.Open();
            }
        }
    }
}
