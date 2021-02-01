using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public enum PlayerStatusEnum {
        Init = 0,
        Playing,
        Paused,
        End,
    }


    static public class PlayerStatusEnumExtesion {

        static public bool IsPlaying(this PlayerStatusEnum status) {
            return status == PlayerStatusEnum.Playing;
        }

        static public bool IsPaused(this PlayerStatusEnum status) {
            return status == PlayerStatusEnum.Paused;
        }
    }
}
