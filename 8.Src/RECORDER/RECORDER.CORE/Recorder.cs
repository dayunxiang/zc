using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public class Recorder {

        public DateTime StartDateTime { get; private set; }

        public DateTime LastSaveDateTime { get; private set; }

        public Record Record { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<SavingFrameEventArgs> SavingFrame;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SavingRecord;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler StatusChanged;


        private TimeSpan _frameTimeSpan;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frameTimeSpan"></param>
        public Recorder(TimeSpan frameTimeSpan) {
            Debug.Assert(
                frameTimeSpan >= TimeSpan.FromSeconds(0.1) &&
                frameTimeSpan <= TimeSpan.FromSeconds(2.0)
                );

            this._frameTimeSpan = frameTimeSpan;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Start() {
            if (this.Status == RecorderStatusEnum.Idle) {
                this.StartDateTime = DateTime.Now;
                this.Record = new Record(this.StartDateTime, _frameTimeSpan);
                this.Status = RecorderStatusEnum.Recording;
                return true;
            } else {
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private void SaveFrame() {
            OnSaveFrame();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void SaveRecord(Action<Record> action) {
            action(this.Record);
        }

        /// <summary>
        /// 
        /// </summary>
        public void SaveRecord() {
            OnSaveRecord();
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSaveRecord() {
            if (SavingRecord != null) {
                SavingRecord(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnSaveFrame() {
            if (SavingFrame != null) {
                var eventArgs = new SavingFrameEventArgs();
                SavingFrame(this, eventArgs);

                Debug.Assert(eventArgs.Frame != null);

                this.Record.Frames.Add(eventArgs.Frame);
                this.LastSaveDateTime = DateTime.Now;
            } else {
                throw new InvalidOperationException("not listen saving frame event");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Stop() {
            if (this.Status == RecorderStatusEnum.Recording) {
                this.Status = RecorderStatusEnum.Idle;
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RecorderStatusEnum Status {
            get { return _status; }
            set {
                if (_status != value) {
                    _status = value;
                    OnStatusChanged();
                }
            }
        } private RecorderStatusEnum _status = RecorderStatusEnum.Idle;

        /// <summary>
        /// 
        /// </summary>
        private void OnStatusChanged() {
            if (StatusChanged != null) {
                StatusChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Check() {
            if (this.Status == RecorderStatusEnum.Recording) {
                var ts = DateTime.Now - this.LastSaveDateTime;
                if (ts >= _frameTimeSpan) {
                    SaveFrame();
                    this.LastSaveDateTime = DateTime.Now;
                }
            }
        }
    }
}
