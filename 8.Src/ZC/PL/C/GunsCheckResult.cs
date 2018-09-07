using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using NLog;

namespace PL
{
    public enum GunsCheckResult
    {
        Working = 0,
        Timeout = 1,
    }
}
