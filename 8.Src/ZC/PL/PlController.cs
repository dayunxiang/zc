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
        private bool _isWorking;
        private DateTime _beginDt;
        private DateTime _endDt;
        private GunsController _gunsController;

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
            if(!_isWorking)
            {
                _isWorking = true;
                _beginDt = DateTime.Now;
                // 1. get guns -> working guns
                // 2. guns open
                var gunsController = GetGunsController();
                gunsController.Start();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private GunsController GetGunsController()
        {
            if(_gunsController == null)
            {
                var guns = App.GetApp().Dams.GetFirstGuns(this.PlOptions);
                _gunsController = new GunsController(guns);
            }
            return _gunsController;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isToTail"></param>
        /// <returns></returns>
        private List<Gun> GetNextGuns(out bool isToTail)
        {
            // todo: set to tail
            //
            isToTail = false;
            return GetGunsController().GetNextGuns();
            //if(_gunsController == null)
            //{
            //    // not start
            //    //
            //}
            //else
            //{
            //    // started
            //    //
            //    return _gunsController.GetNextGuns();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        internal PlCheckResult Check()
        {
            //throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
