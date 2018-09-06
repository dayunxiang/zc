using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PL;
using NUnit.Framework;

namespace PL.Test
{

    [TestFixture]
    public class Test
    {
        [Test]
        public void test()
        {
            var dams = App.GetApp().Dams;
            var dam0 = dams.First.Value;
            var dam = dam0;
            var nextDamCount = 0;
            while(dam != null)
            {
                Console.WriteLine(dam.Name);
                nextDamCount++;
                dam = dam.GetNextDam();
            }
            Assert.AreEqual(4, nextDamCount);


            Assert.AreEqual(dams.Count, 4);
            Assert.AreEqual(dams.First.Value.Guns.Count, 10);


            Console.WriteLine("aaa");
        }
    }
}
