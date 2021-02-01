using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

            this.tsbRecordList.Click += tsbRecordList_Click;
            UpdateButtonsStatus(PlayerStatusEnum.Init);
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


        private void UpdateButtonsStatus(PlayerStatusEnum playerStatus) {
            this.tsbPlay.Enabled = !playerStatus.IsPlaying();
            this.tsbPause.Enabled = playerStatus.IsPlaying();
            this.tsbStop.Enabled = playerStatus.IsPlaying() || playerStatus.IsPaused();

            this.tsbPrevRecord.Enabled = false;
            this.tsbNextRecord.Enabled = false;

            this.tsbPrevFrame.Enabled = false;//playerStatus.IsPlaying();
            this.tsbNextFrame.Enabled = false;//playerStatus.IsPlaying();

            if (playerStatus == PlayerStatusEnum.Init) {
                this.tbRecord.Value = 0;
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
            this.tbRecord.Maximum = player.Record.Frames.Count;
            this.lblRecordFileValue.Text = player.RecordInfoNode.Value.Name;
            this.lblRecordFileSizeValue.Text = player.RecordInfoNode.Value.Size.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_PlayedFrame(object sender, PlayFrameEventArgs e) {
            this.tbRecord.Value = e.FrameIndex;
            var player = sender as Player;
            this.lblPositionValue.Text = string.Format("{0} / {1}", e.FrameIndex, player.Record.Frames.Count);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void player_StatusChanged(object sender, EventArgs e) {
            Player p = (Player)sender;
            UpdateButtonsStatus(p.Status);
        }

        private void tsbNextFrame_Click(object sender, EventArgs e) {

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

        private void tsbPrevFrame_Click(object sender, EventArgs e) {

        }

        private void tsbNextRecord_Click(object sender, EventArgs e) {

        }

        private void tsbSpeed_Click(object sender, EventArgs e) {

        }
    }
}
