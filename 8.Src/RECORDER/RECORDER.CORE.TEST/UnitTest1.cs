using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace RECORDER.CORE.TEST {
    [TestClass]
    public class RecorderCreateTest{
        private Random _random = new Random();
        private Dictionary<int, string> _indexNameDict = new Dictionary<int, string>();

        [TestMethod]
        public void CreateRecordTest() {

            
            Console.WriteLine("test");

            var record = CreateRecord(); 
            var json = JsonConvert.SerializeObject(record);

            //Console.WriteLine(json);
            var sw = Stopwatch.StartNew();
            File.WriteAllText("record_test.json",json);
            sw.Stop();
            Console.WriteLine("write json use time: " + sw.Elapsed);
        }

        private Record CreateRecord() {
            var r = new Record(DateTime.Now, TimeSpan.FromSeconds(0.5));
            //var framesCount = 20 * 60 * 2;

            var framesCount = 2;
            while (framesCount-- > 0) {
                r.Frames.Add(CreateFrame());
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Frame CreateFrame() {
            var f = new Frame();

            //int itemsCount = 200;
            int itemsCount = 2;
            for (int i = 0; i < itemsCount; i++) {
                var nvp = CreateNameValuePair(i);
                f.NameValuePairs.Add(nvp);
            }
            return f;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private NameValuePair CreateNameValuePair(int idx) {
            var name = GetName(idx);
            double value = _random.NextDouble() * 10000;
            var nvp = new NameValuePair(name, value, TypeCode.Double);
            return nvp;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private string GetName(int idx) {
            if (_indexNameDict.ContainsKey(idx)) {
                return _indexNameDict[idx];
            } else {
                var buffer = new byte[6];
                _random.NextBytes(buffer);
                var name = "name" + BitConverter.ToString(buffer);
                _indexNameDict[idx] = name;
                return name;
            }
        }
    }


    [TestClass]
    public class RecordDeserializeTest {
        [TestMethod]
        public void Test() {

            var sw = new Stopwatch();
            var json = File.ReadAllText("record_test.json");
            sw.Stop();
            Console.WriteLine("read json file, use timespan: " + sw.Elapsed);

            sw.Restart();
            var record = JsonConvert.DeserializeObject<Record>(json);
            Assert.IsNotNull(record);
            Console.WriteLine(record.StartDateTime);
            sw.Stop();
            Console.WriteLine("deserialize json: {0} size, use: {1}", json.Length, sw.Elapsed);
        }
    }
}
