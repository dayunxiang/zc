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

        /// <summary>
        /// 
        /// </summary>
        public MaterialHeapPositionDefine Define { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal ReadStartPosition() {
            var r = OpcServerManager.Instance.OpcServer.Read(this.Define.StartPositionAddress);
            return Convert.ToDecimal(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public decimal ReadEndPosition() {
            var r = OpcServerManager.Instance.OpcServer.Read(this.Define.EndPositionAddress);
            return Convert.ToDecimal(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ReadAttribute() {
            var r = OpcServerManager.Instance.OpcServer.Read(this.Define.AttributeAddress);
            return Convert.ToInt32(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int ReadMaterialId() {
            var r = OpcServerManager.Instance.OpcServer.Read(this.Define.IdAddress);
            return Convert.ToInt32(r);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsInRange(decimal location) {
            return
                location > this.ReadStartPosition() &&
                location < this.ReadEndPosition();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanWet() {
            int attribute = ReadAttribute();
            return attribute == (int)CanWetEnum.Yes;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanNotWet() {
            return !CanWet();
        }
    }
}
