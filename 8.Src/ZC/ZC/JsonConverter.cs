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
using ZC.Argument;

namespace ZC
{

    public class JsonConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        static public string ToString(ItemList items)
        {
            var sw = new StringWriter();
            var j = new JsonSerializer();
            j.Serialize(sw, items);
            return sw.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public ItemList ToObject(string s)
        {
            var sr = new StringReader(s);
            var j = new JsonSerializer();
            return (ItemList) j.Deserialize(sr, typeof(ItemList));
        }
    }
}
