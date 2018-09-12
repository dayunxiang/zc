using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace PLC
{
    public class OpcException : Exception
    {
        public OpcException(string message)
            : base(message)
        {

        }

        public OpcException(string message, Exception innerException)
            :base(message, innerException)
        {

        }
    }
}
