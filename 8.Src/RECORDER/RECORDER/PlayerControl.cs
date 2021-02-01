using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Xdgk.Common;
using RECORDER.CORE;

namespace RECORDER {
    public partial class PlayerControl : UserControl {

        private Player _player;

        public PlayerControl() {
            InitializeComponent();

            this.tsbPlay.Image = RECORDER.Properties.Resources.control_play_blue.ToBitmap();
            this.tsbPause.Image = RECORDER.Properties.Resources.control_pause_blue.ToBitmap();
            this.tsbStop.Image = RECORDER.Properties.Resources.control_stop_blue.ToBitmap();
            this.tsbPrevRecord.Image = RECORDER.Properties.Resources.control_previous_record_blue.ToBitmap();
            this.tsbPrevFrame.Image = RECORDER.Properties.Resources.control_rewind_blue.ToBitmap();
            this.tsbNextFrame.Image = RECORDER.Properties.Resources.control_fastforward_blue.ToBitmap();
            this.tsbNextRecord.Image = RECORDER.Properties.Resources.control_next_record_blue.ToBitmap();
            this.tsbRecordList.Image = RECORDER.Properties.Resources.text_list_bullets;

            this.tsbSpeed.ComboBox.DisplayMember = "Key";
            this.tsbSpeed.ComboBox.ValueMember = "Value";
            this.tsbSpeed.ComboBox.DataSource = CreateSpeedDataSource();
            this.tsbSpeed.ComboBox.SelectedIndex = 1;


            this.tsbRecordList.Click += tsbRecordList_Click;
            UpdateControlsStatus(PlayerStatusEnum.Init);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private KeyValueCollection CreateSpeedDataSource() {
            var kvs = new KeyValueCollection();
            kvs.Add("x 0.5", 0.5m);
            kvs.Add("x 1.0", 1.0m);
            kvs.Add("x 2.0", 2.0m);
            kvs.Add("x 4.0", 4.0m);
            kvs.Add("x 8.0", 8.0m);
            return kvs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbRecordList_Click(object sender, EventArgs e) {
            frmRecordList f = frmRecordList.Instance;
            f.Show();
            f.Activate();

            //this.tsbRecordList.Checked = true;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerStatus"></param>
        private void UpdateControlsStatus(PlayerStatusEnum playerStatus) {
            this.tsbPlay.Enabled = !playerStatus.IsPlaying();
            this.tsbPause.Enabled = playerStatus.IsPlaying();
            this.tsbStop.Enabled = playerStatus.IsPlaying() || playerStatus.IsPaused();

            this.tsbPrevRecord.Enabled = false;
            this.tsbNextRecord.Enabled = false;

            this.tsbPrevFrame.Enabled = playerStatus.IsPaused();
            this.tsbNextFrame.Enabled = playerStatus.IsPaused();

            if (playerStatus == PlayerStatusEnum.Init) {
                this.tbRecord.Value = 0;

                if (_player != null) {
                    this.lblPositionValue.Text = string.Format("{0} / {1}", 0, _player.Record.Frames.Count);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Player Player {
            get { return _player; }
            set {
                if (_player != value) {
                    if (_player != null) {
                        UnregisterPlayerEvents(_player);
                    }

                    _player = value;
                    RegisterPlayerEvents(_player);
                }
            }
        }

        private void UnregisterPlayerEvents(CORE.Player player) {
            player.PlayedFrame -= player_PlayedFrame;
            player.StatusChanged -= player_StatusChanged;
            player.RecordInfoNodeChanged -= player_RecordInfoNodeChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        private void RegisterPlayerEvents(CORE.Player player) {
            player.PlayedFrame += player_PlayedFrame;
            player.StatusChanged += player_StatusChanged;
            player.RecordInfoNodeChanged += player_RecordInfoNodeChanged;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_RecordInfoNodeChanged(object sender, EventArgs e) {
            var player = sender as Player;
            this.tbRecord.Minimum = 0;
            this.tbRecord.Maximum = player.Record.Frames.Count - 1;
            this.tbRecord.SmallChange = CalcSmallChange(this.tbRecord);
            this.tbRecord.LargeChange = CalcLargeChange(this.tbRecord);
            this.lblRecordFileValue.Text = player.RecordInfoNode.Value.Name;
            this.lblRecordFileSizeValue.Text = player.RecordInfoNode.Value.Size.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trackBar"></param>
        /// <returns></returns>
        private int CalcLargeChange(TrackBar tb) {
            var lc = (tb.Maximum - tb.Minimum) / 10;
            lc = lc < 1 ? 1 : lc;
            return lc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tb"></param>
        /// <returns></returns>
        private int CalcSmallChange(TrackBar tb) {
            var sc = (tb.Maximum - tb.Minimum) / 100;
            sc = sc < 1 ? 1 : sc;
            return sc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_PlayedFrame(object sender, PlayFrameEventArgs e) {
            this.tbRecord.Value = e.Frame.FrameIndex;
            var player = sender as Player;
            this.lblPositionValue.Text = string.Format("{0} / {1}", e.Frame.FrameIndex, player.Record.Frames.Count);
            this.lblDateTimeValue.Text = e.Frame.DateTime.ToString ("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_StatusChanged(object sender, EventArgs e) {
            Player p = (Player)sender;
            UpdateControlsStatus(p.Status);
        }

        private void tsbPrevFrame_Click(object sender, EventArgs e) {
            _player.PrevFrame();
        }
        private void tsbNextFrame_Click(object sender, EventArgs e) {
            _player.NextFrame();
        }

        private void tsbPlay_Click(object sender, EventArgs e) {
            _player.Play();
        }

        private void tsbPause_Click(object sender, EventArgs e) {
            _player.Pause();
        }

        private void tsbStop_Click(object sender, EventArgs e) {
            _player.Stop();
        }

        private void tsbPrevRecord_Click(object sender, EventArgs e) {
//_player.n
        }


        private void tsbNextRecord_Click(object sender, EventArgs e) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbRecord_Scroll(object sender, EventArgs e) {
            var tb = sender as TrackBar;

            Console.WriteLine("tbRecord_Scroll: " + tb.Value);

            _player.SetScroll(tb.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSpeed_SelectedIndexChanged(object sender, EventArgs e) {
            decimal speed = (decimal)((KeyValue)this.tsbSpeed.SelectedItem).Value;
            if (this._player != null) {
                this._player.Speed = speed;
            }
        }
    }
}
