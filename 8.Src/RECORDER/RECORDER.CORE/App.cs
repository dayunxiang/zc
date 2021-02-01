using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {
    public class App {

        static public App Instance = new App();
        private App() {
            this.Recorder = new Recorder(TimeSpan.FromSeconds(0.5));
            this.Player = new Player();
        }

        public Player Player {
            get;
            private set;
        }

        public Recorder Recorder {
            get;
            private set;
        }
    }
}
