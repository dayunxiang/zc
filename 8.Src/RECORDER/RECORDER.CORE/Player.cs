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
        private Frame _currentFrame;
        private Frame _nextFrame;
        private DateTime _currentFramePlayDatetime;


        private Timer _timer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="record"></param>
        public Player() {
            this.Speed = 1.0m;
            this.Status = PlayerStatusEnum.Init;

            this._currentFrame = null;
            this._nextFrame = null;
            //this._nextFrameIndex = -1;

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
                        if (this.Status.IsPlaying() || this.Status.IsPaused()) {
                            Stop();
                        }
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
                Frame first = GetFirstFrame();
                if (first != null) {
                    SetCurrentFrame(first);
                    this.Status = PlayerStatusEnum.Playing;

                    PlayCurrentFrame();

                    SetNextFrame();
                    this._timer.Start();
                    return true;
                } else {
                    return false;
                }

                //if (FillNextFrame()) {
                //    this.PlayDateTime = DateTime.Now;
                //    this.Status = PlayerStatusEnum.Playing;
                //    this._timer.Start();
                //    return true;
                //} else {
                //    return false;
                //}
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool SetNextFrame() {
            var nextIndex = this._currentFrame.FrameIndex + 1;
            this._nextFrame = this.Record.GetFrame(nextIndex);
            return this._nextFrame != null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        private void SetCurrentFrame(Frame frame) {
            this._currentFrame = frame;
        }

        /// <summary>
        /// 
        /// </summary>
        private void PlayCurrentFrame() {
            OnPlayingFrame(_currentFrame);
            OnPlayedFrame(_currentFrame);
            this._currentFramePlayDatetime = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Frame GetFirstFrame() {
            return this.Record.GetFirstFrame();
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
                //this._startFrameIndex = _nextFrameIndex - 1;
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
                //this._startFrameIndex = 0;
                //this._nextFrameIndex = -1;
                this._currentFrame = null;
                this._nextFrame = null;
                this._currentFramePlayDatetime = DateTime.MinValue;
                this.PlayDateTime = DateTime.MinValue;
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
                TimeSpan currentFramePlayTimeSpan = DateTime.Now - this._currentFramePlayDatetime;
                TimeSpan recordTimeSpan = TimeSpan.FromTicks((long)(currentFramePlayTimeSpan.Ticks * this.Speed));

                DateTime recordDateTime = _currentFrame.DateTime + recordTimeSpan;

                if (_nextFrame == null) {
                    Stop();
                    return true;
                }

                if (recordDateTime >= _nextFrame.DateTime) {
                    SetCurrentFrame(_nextFrame);
                    PlayCurrentFrame();
                    SetNextFrame();
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
        private void OnPlayedFrame(Frame frame) {
            if (PlayedFrame != null) {
                PlayedFrame(this, new PlayFrameEventArgs(frame));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="frame"></param>
        /// <param name="no"></param>
        protected void OnPlayingFrame(Frame frame) {
            if (PlayingFrame != null) {
                PlayingFrame(this, new PlayFrameEventArgs(frame));
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool SetScroll(int scrollValue) {
            Frame frame = this.Record.GetFrame(scrollValue);
            if (frame != null) {
                SetCurrentFrame(frame);
                PlayCurrentFrame();
                SetNextFrame();
                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool NextFrame() {
            if (_nextFrame != null) {
                SetCurrentFrame(_nextFrame);
                PlayCurrentFrame();
                SetNextFrame();
                return true;
            } else {
                return false;
            }
        }

        public bool PrevFrame() {
            if (_currentFrame.FrameIndex > 0) {
                Frame prevFrame =  _record.GetFrame(_currentFrame.FrameIndex - 1);
                SetCurrentFrame(prevFrame);
                PlayCurrentFrame();
                SetNextFrame();
                return true;
            } else {
                return false;
            }
        }
    }
}
