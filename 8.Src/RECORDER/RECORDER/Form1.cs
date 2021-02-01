using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RECORDER.CORE;

namespace RECORDER {
    public partial class Form1 : Form {

        private Recorder _recorder = new Recorder(TimeSpan.FromSeconds(0.5));
        private frmDatas _frmDatas;

        /// <summary>
        /// 
        /// </summary>
        public Form1() {
            InitializeComponent();

            var player = App.Instance.Player;
            player.PlayingFrame += _player_PlayingFrame;

            this.playerControl1.Player = player;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void _recorder_SavingFrame(object sender, SavingFrameEventArgs e) {
        //    var f = new Frame();
        //    var ctrls = new CheckBox[] { 
        //        this.checkBox1,
        //        this.checkBox2,
        //        this.checkBox3,
        //        this.checkBox4,
        //        this.checkBox5,
        //        this.checkBox6,
        //        this.checkBox7,
        //        this.checkBox8,
        //        this.checkBox9,
        //        this.checkBox10,
        //        this.checkBox11,
        //        this.checkBox12,
        //        this.checkBox13,
        //        this.checkBox14,
        //        this.checkBox15,
        //        this.checkBox16,
        //        this.checkBox17,
        //        this.checkBox18,
        //        this.checkBox19,
        //        this.checkBox20,
        //    };

        //    new List<CheckBox>(ctrls).ForEach(c => 
        //        f.NameValuePairs.Add(
        //            new NameValuePair(c.Name, c.Checked, TypeCode.Boolean)
        //        ));

        //    e.Frame = f;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void timer1_Tick(object sender, EventArgs e) {
        //    if (_recorder.Status.IsRecording()) {
        //        _recorder.Check();
        //    }

        //    if (_player != null && _player.Status.IsPlaying()) {
        //        _player.Check();
        //    }
        //}

        private void Form1_Load(object sender, EventArgs e) {
            _frmDatas = new frmDatas();
            _frmDatas.Show();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnRecordStart_Click(object sender, EventArgs e) {
        //    if (this._recorder.Status.IsIdle()) {
        //        this._recorder.Start();

        //        this.timer1.Start();
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnRecordStop_Click(object sender, EventArgs e) {
        //    if (this._recorder.Status.IsRecording()) {
        //        this._recorder.Stop();
        //    }
        //}

        //private void btnSave_Click(object sender, EventArgs e) {
        //    this._recorder.SaveRecord(r => {
        //        var json = Newtonsoft.Json.JsonConvert.SerializeObject(r);
        //        File.WriteAllText(
        //            DateTime.Now.ToFileTime().ToString(), 
        //            json);
        //    });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnPlay_Click(object sender, EventArgs e) {
        //    var record = Record.FromJsonFile("record_test.json");
        //    _frmDatas.SetRecord(record);

        //    _player = new Player();
        //    //_player.Speed = 0.25m;
        //    _player.PlayingFrame += _player_PlayingFrame;
        //    _player.Play();

        //    timer1.Start();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _player_PlayingFrame(object sender, PlayFrameEventArgs e) {
            Console.WriteLine("play frame: " + e.FrameIndex);
            //this.dgv.DataSource = e.Frame.NameValuePairs;
            _frmDatas.SetFrame(e.Frame, e.FrameIndex);
        }
    }
}
