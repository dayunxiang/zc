using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL;
using NUnit.Framework;

namespace PL.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            PL.Hardware.Serializer.test();
            Console.Read();

            var app = App.GetApp();
            Console.ReadKey();
        }
    }

}
