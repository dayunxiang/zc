using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Dam
    {
        /// <summary>
        /// 
        /// </summary>
        public Dam()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public int No
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public LinkedListNode<Dam> DamNode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dam GetNextDam()
        {
            var next = this.DamNode.Next;
            if(next != null)
            {
                return next.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public LinkedList<Dam> GetOwnerDamList()
        {
            return this.DamNode.List;
        }

        /// <summary>
        /// 
        /// </summary>
        public GunLinkedList Guns
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public GunList GetFirstGuns(int count)
        {
            return Guns.GetFirstGuns(count);
        }
    }
}
