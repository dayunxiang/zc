using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using NUnit.UiKit;
using Xdgk.Common;
using NLog;
using Newtonsoft.Json;

namespace ZC.Argument
{
    public class Item
    {
        public string Command { get; set; }
        public string Value { get; set; }
    }
}
