using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLForm {
    public class S {
        private S() {
        }

        public static readonly string InvalidSn = "无效的注册码";
        public static readonly string ProgramRunning = "程序已经启动";
        public static readonly string CannotExit = "工作中, 无法退出!";
        public static readonly string SureExit= "确定要退出吗?";
        public static readonly string OpcNotConnected = "OPC 未连接";
        public static readonly string OpcConnectedWithDt = "OPC 已经连接, {0}";
        public static readonly string LogFileSaved = "日志保存成功";
    }
}
