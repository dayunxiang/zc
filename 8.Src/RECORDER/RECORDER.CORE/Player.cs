using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RECORDER.CORE {

    public class Player {

        public event EventHandler<PlayFrameEventArgs> PlayingFrame;
        public event EventHandler<PlayFrameEventArgs> PlayedFrame;

        public event EventHandler StatusChanged;
        public event EventHandler RecordInfoNodeChanged;

        private const decimal SPEED_MAX = 8.0m;
        private const decimal SPEED_MIN = 0.5m;

        private Record _record;
        private Frame _nextFrame;
        private int _nextFrameIndex;
        private int _startFrameIndex;
        private Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        public Player() {
            //if (record == null) {
            //    throw new ArgumentNullException("record");
            //}
            //this.Record = record;
            this.Speed = 1.0m;
            this.Status = PlayerStatusEnum.Init;

            this._nextFrame = null;
            this._nextFrameIndex = -1;

            this._timer = new Timer();
            this._timer.Interval = 30;
            this._timer.Tick += _timer_Tick;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Tick(object sender, EventArgs e) {
            this.Check();
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime PlayDateTime {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Record Record {
            get {
                return _record;
            }
        }

        public LinkedListNode<RecordInfo> RecordInfoNode {
            get { return _recordInfoNode; }
            set {
                if (_recordInfoNode != value) {
                    _recordInfoNode = value;

                    if (_recordInfoNode != null) {
                        _record = Record.FromJsonFile(_recordInfoNode.Value.Name);
                    }
                    OnRecrodInfoNodeChanged();
                }
            }
        } private LinkedListNode<RecordInfo> _recordInfoNode;

        /// <summary>
        /// 
        /// </summary>
        private void OnRecrodInfoNodeChanged() {
            if (RecordInfoNodeChanged != null) {
                RecordInfoNodeChanged(this, EventArgs.Empty);
            }
        }

        //public LinkedListNode<Record> RecordNode {
        //    get { return _recordLinkedListNode; }
        //    set {
        //        if (_recordLinkedListNode != value) {
        //            _recordLinkedListNode = value;
        //        }
        //    }
        //}

        /// <summary>
        /// 0.5 1 2 4 8
        /// </summary>
        public decimal Speed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PlayerStatusEnum Status {
            get { return _status; }
            set {
                if (this._status != value) {
                    _status = value;
                    OnStatusChanged();
                }
            }
        } private PlayerStatusEnum _status = PlayerStatusEnum.Init;

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
        /// <returns></returns>
        public bool Play() {
            if (this.Status.IsPaused()) {
                this.Continue();
                return true;
            }

            if (this.Status == PlayerStatusEnum.Init || this.Status == PlayerStatusEnum.End) {
                if (FillNextFrame()) {
                    this.PlayDateTime = DateTime.Now;
                    this.Status = PlayerStatusEnum.Playing;
                    this._timer.Start();
                    return true;
                } else {
                    return false;
                }
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Pause() {
            if (this.Status == PlayerStatusEnum.Playing) {
                this.Status = PlayerStatusEnum.Paused;
                return true;
            } else {
                return false;
            }
        }

        public bool Continue() {
            if (this.Status.IsPaused()) {
                this.PlayDateTime = DateTime.Now;
                this._startFrameIndex = _nextFrameIndex - 1;
                this.Status = PlayerStatusEnum.Playing;

                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Stop() {
            if (this.Status == PlayerStatusEnum.Playing ||
                    this.Status == PlayerStatusEnum.Paused) {
                this.Status = PlayerStatusEnum.Init;
                this._startFrameIndex = 0;
                this._nextFrameIndex = -1;
                this._nextFrame = null;
                this._timer.Stop();
                return true;
            } else {
                return false;
            }
        }

        public bool Reset() {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Check() {
            if (this.Status == PlayerStatusEnum.Playing) {
                TimeSpan playTimeSpan = DateTime.Now - this.PlayDateTime;
                TimeSpan recordTimeSpan = TimeSpan.FromTicks((long)(playTimeSpan.Ticks * this.Speed));

                int no = (int)(recordTimeSpan.Ticks / this.Record.FrameTimeSpan.Ticks) + _startFrameIndex;
                if (no >= _nextFrameIndex) {
                    OnPlayingFrame(_nextFrame, _nextFrameIndex);

                    OnPlayedFrame(_nextFrame, _nextFrameIndex);
                    if (FillNextFrame()) {

                    } else {
                        Stop();
                    }
                }
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="frameIndex"></param>
        private void OnPlayedFrame(Frame frame, int frameIndex) {
            if (PlayedFrame != null) {
                PlayedFrame(this, new PlayFrameEventArgs(frame, frameIndex));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool FillNextFrame() {
            _nextFrameIndex++;

            if (_nextFrameIndex < this.Record.Frames.Count) {
                _nextFrame = this.Record.Frames[(int)_nextFrameIndex];
                return true;
            } else {
                _nextFrameIndex = -1;
                _nextFrame = null;
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="no"></param>
        protected void OnPlayingFrame(Frame frame, int no) {
            if (PlayingFrame != null) {
                PlayingFrame(this, new PlayFrameEventArgs(frame, no));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SpeedIncrease() {
            if (this.Speed != SPEED_MAX) {
                this.Speed *= 2;
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SpeedDecrease() {
            if (this.Speed != SPEED_MIN) {
                this.Speed /= 2;
                return true;
            } else {
                return false;
            }
        }
    }
}
