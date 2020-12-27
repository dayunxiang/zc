using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using PL.Hardware;

namespace PL {

    public class Dam {
        /// <summary>
        /// 
        /// </summary>
        public Dam() {
            this.DamAreas = new DamAreaList();
            this.AssociateCarts = new CartList();
        }

        /// <summary>
        /// 
        /// </summary>
        public int No {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public LinkedListNode<Dam> DamNode {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// DB2  - 1
        /// DB3  - 2
        /// DB4  - 4
        /// DBQ3 - 8
        /// </returns>
        public int GetDamValue() {
            return Convert.ToInt32(Math.Pow(2, this.No));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dam GetNextDam() {
            var next = this.DamNode.Next;
            if (next != null) {
                return next.Value;
            } else {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public LinkedList<Dam> GetOwnerDamList() {
            return this.DamNode.List;
        }

        /// <summary>
        /// 
        /// </summary>
        public GunLinkedList Guns {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DamAreaList DamAreas {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public CartList AssociateCarts { 
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public WorkGunGroup GetFirstGuns(int count) {
            return Guns.GetFirstGuns(count);
        }
    }
}
