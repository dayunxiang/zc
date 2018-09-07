using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLC
{

    /// <summary>
    /// 
    /// </summary>
    public class Lm
    {
        static public readonly List<ILog> Logs = new List<ILog>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        static public void D(string s)
        {
            foreach(var log in Logs)
            {
                log.D(s);
            }
        }
    }
}
