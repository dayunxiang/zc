using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PL;

namespace Test {
    [TestClass]
    public class GunTest{
        [TestMethod]
        public void TestArea() {
            Console.WriteLine("test area");

            var gun = new Gun();
            Console.WriteLine(gun.Area.ToString());

            Assert.IsTrue(gun.Area.CanWet());

            Material.Empty.CanWet = false;
            Assert.IsFalse(gun.Area.CanWet());
        }
    }


    [TestClass]
    public class DefineTest{
        [TestMethod]
        public void Test() {
            //var d = new PL.Hardware.Define();
            var d = Serializer.CreateDefine();
            Console.WriteLine(d.ToJson());
        }
    }
}
