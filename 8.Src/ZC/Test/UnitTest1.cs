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
            //Console.WriteLine(gun.Area.ToString());

            //Assert.IsTrue(gun.Area.CanWet());

            //Material.Empty.CanWet = false;
            //Assert.IsFalse(gun.Area.CanWet());
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


    [TestClass]
    public class AppTest{
        [TestMethod]
        public void Test() {
            var app = App.GetApp();
            Assert.IsTrue(app.Dams.Count > 0);
            Assert.IsTrue(app.Carts.Count > 0);
            Assert.IsTrue(app.MaterialHeaps.Count>0);

            var appController = new AppController(app, app.Gc);
            var plOptions = new PlOptions();
            var plController = new PlController(appController, plOptions);
            appController.AutoManualStatus.Read();
            Console.WriteLine(appController.AutoManualStatus);
        }
    }

}
