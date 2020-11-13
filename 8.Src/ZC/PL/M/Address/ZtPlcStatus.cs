using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class ZtPlcStatus : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public ZtPlcStatus(string address)
            : base(address)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ZtPlcStatusEnum Read()
        {
            if (Config.IsMock)
            {
                return ReadMock();
            }
            else
            {
                return ReadFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ZtPlcStatusEnum ReadFact()
        {
            var v = ReadFromOpc();
            var n = Convert.ToInt32(v);
            return (ZtPlcStatusEnum)n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ZtPlcStatusEnum ReadMock()
        {
            //todo:
            if (ZtPlcStatusUI.Text.Length == 0)
            {
                return ZtPlcStatusEnum.Stop;
            }

            int n = int.Parse(ZtPlcStatusUI.Text);
            return (ZtPlcStatusEnum)n;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztPlcStatusEnum"></param>
        internal void Write(ZtPlcStatusEnum ztPlcStatusEnum)
        {
            if (Config.IsMock)
            {
                WriteMock(ztPlcStatusEnum);
            }
            else
            {
                WriteFact(ztPlcStatusEnum);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztPlcStatusEnum"></param>
        private void WriteFact(ZtPlcStatusEnum ztPlcStatusEnum)
        {
            WriteToOpc((int)ztPlcStatusEnum);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztPlcStatusEnum"></param>
        private void WriteMock(ZtPlcStatusEnum ztPlcStatusEnum)
        {
            this.ZtPlcStatusUI.Text = ((int)ztPlcStatusEnum).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public TextBox ZtPlcStatusUI
        {
            get;
            set;
        }
    }
}
