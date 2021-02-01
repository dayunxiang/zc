using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RECORDER.CORE {

    public enum RecorderStatusEnum {
        Idle = 0,
        Recording,
    }

    static public class RecorderStatusEnumExtension {
        static public bool IsIdle(this RecorderStatusEnum status) {
            return status == RecorderStatusEnum.Idle;
        }

        static public bool IsRecording(this RecorderStatusEnum status) {
            return status == RecorderStatusEnum.Recording;
        }
    }
}
