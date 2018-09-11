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

        #region Members
        static private Logger _logger = LogManager.GetCurrentClassLogger();

        private bool _isWorking;
        private DateTime _beginDt;
        private DateTime _endDt;
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
        public bool IsWorking()
        {
            return _isWorking;
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
            if (!_isWorking)
            {
                _isWorking = true;
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
                        gunsController.Close();
                        return PlCheckResult.Completed;
                    }
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
            if(IsWorking())
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
