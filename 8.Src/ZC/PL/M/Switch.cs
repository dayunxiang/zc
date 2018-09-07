using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PLC;

namespace PL
{
    public class Switch : PlcAddress
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="address"></param>
        public Switch(string address)
            : base(address)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public ItemDefine ItemDefine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOpened
        {
            get
            {
                if (Config.IsMock)
                {
                    return IsOpenedMock();
                }
                else
                {
                    return IsOpenedFact();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (Config.IsMock)
            {
                OpenMock();
            }
            else
            {
                OpenFact();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (Config.IsMock)
            {
                CloseMock();
            }
            else
            {
                CloseFact();
            }
        }

        #region fact
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsOpenedFact()
        {
            var val = ReadFromOpc();
            var n = Convert.ToInt32(val);
            return n == (int)GunStatus.Open;
        }

        /// <summary>
        /// 
        /// </summary>
        private void OpenFact()
        {
            WriteToOpc((int)GunStatus.Open);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseFact()
        {
            WriteToOpc((int)GunStatus.Close);
        }
        #endregion //fact

        #region mock
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsOpenedMock()
        {
            return SwitchUI.BackColor == Color.Green;
        }

        /// <summary>
        /// 
        /// </summary>
        private void CloseMock()
        {
            this.SwitchUI.BackColor = SystemColors.Control;
        }


        /// <summary>
        /// 
        /// </summary>
        private void OpenMock()
        {
            this.SwitchUI.BackColor = Color.Green;
        }

        /// <summary>
        /// 
        /// </summary>
        public Label SwitchUI
        {
            get;
            set;
        }
        #endregion //mock
    }
}
