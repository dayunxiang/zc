using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL.Hardware;
using PLC;
namespace PL {

    public class MaterialHeapPosition {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        public MaterialHeapPosition(MaterialHeapPositionDefine define) {
            if (define == null) {
                throw new ArgumentNullException("materialHeapPositionDefine");
            }

            this.Define = define;
        }

        public MaterialHeapPositionDefine Define { get; private set; }

        public bool IsReadedFromPlc { get; private set; }
        public DateTime ReadDateTime { get; private set; }

        public decimal StartPosition { get; set; }
        public decimal EndPosition { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsInRange(decimal location) {
            return
                location > this.StartPosition &&
                location < this.EndPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ReadFromPlc() {
            var itemNames = new string[]{
                this.Define.StartPositionAddress,
                this.Define.EndPositionAddress,
            };

            var values = OpcServerManager.Instance.OpcServer.Read(itemNames);

            this.StartPosition = Convert.ToDecimal(values[0]);
            this.EndPosition = Convert.ToDecimal(values[1]);

            this.IsReadedFromPlc = true;
            this.ReadDateTime = DateTime.Now;
        }
    }
}
