using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;

namespace PL
{

    public class Gun
    {

        /// <summary>
        /// 
        /// </summary>
        public Gun()
        {

        }

        public int No { get; set; }
        public LinkedListNode<Gun> GunNode { get; set; }
        public Fault Fault { get; set; }
        public Mark Mark { get; set; }
        public Remote Remote { get; set; }
        public Switch Switch { get; set; }
        public Dam Dam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Gun Next
        {
            get
            {
                var nextNode = this.GunNode.Next;
                if(nextNode != null)
                {
                    return nextNode.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        public bool Eq(Gun gun)
        {
            return this.Dam.No == this.Dam.No &&
                this.No == gun.No;
        }

        public bool Gt(Gun gun)
        {
            if(this.Dam.No > gun.Dam.No)
            {
                return true;
            }

            if(this.Dam.No == gun.Dam.No)
            {
                return this.No > gun.No;
            }

            return false;
        }

        public bool Lt(Gun gun)
        {
            if(this.Dam.No < gun.Dam.No)
            {
                return true;
            }

            if(this.Dam.No == gun.Dam.No)
            {
                return this.No < gun.No;
            }

            return false;
        }
    }
}
