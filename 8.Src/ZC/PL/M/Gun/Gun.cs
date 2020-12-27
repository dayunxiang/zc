using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using PL.Hardware;
using NLog;

namespace PL {

    public class Gun {
        #region ctor
        /// <summary>
        /// 
        /// </summary>
        public Gun(GunDefine define) {
            if (define == null) {
                throw new ArgumentNullException("gunDefine");
            }

            this.Define = define;
        }
        #endregion //ctor

        #region Members

        static private Logger _logger = LogManager.GetCurrentClassLogger();
         
        public GunDefine Define { get; private set; }

        public string Name { get { return this.Define.Name; } }
        public int No { get { return this.Define.No; } }
        public LinkedListNode<Gun> GunNode { get; set; }
        public Fault Fault { get; set; }
        public Mark Mark { get; set; }
        public Remote Remote { get; set; }
        public Switch Switch { get; set; }
        //public GunWorkStatus GunWorkStatus { get; set; }
        public Dam Dam { get; set; }

        public DamArea AssociateDamArea { get; set; }
        #endregion //Members

        #region Next
        /// <summary>
        /// 
        /// </summary>
        public Gun Next {
            get {
                var nextNode = this.GunNode.Next;
                if (nextNode != null) {
                    return nextNode.Value;
                } else {
                    return null;
                }
            }
        }
        #endregion //Next

        #region Eq
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        public bool Eq(Gun gun) {
            return this.Dam.No == this.Dam.No &&
                this.No == gun.No;
        }
        #endregion //Eq

        #region Gt
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        public bool Gt(Gun gun) {
            if (this.Dam.No > gun.Dam.No) {
                return true;
            }

            if (this.Dam.No == gun.Dam.No) {
                return this.No > gun.No;
            }

            return false;
        }
        #endregion //Gt

        #region Lt

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gun"></param>
        /// <returns></returns>
        public bool Lt(Gun gun) {
            if (this.Dam.No < gun.Dam.No) {
                return true;
            }

            if (this.Dam.No == gun.Dam.No) {
                return this.No < gun.No;
            }

            return false;
        }
        #endregion //Lt


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsNotCoverCart() {
            return !IsCoverCarts();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsCoverCarts() {
            var carts = GetAssociateCarts();
            foreach (var cart in carts) {
                bool isCover = IsCoverCart(cart);
                if (isCover) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 1. check not fault
        /// 2. check cart location
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        private bool IsCoverCart(Cart cart) {
            var isFault = cart.ReadFault();
            if (isFault) {
                return false;
            }

            var cartLocation = cart.ReadLocation();

            var gunBeginLocation = Math.Max(this.Location - Config.GunRadius, 0m);
            var gunEndLocation = this.Location + Config.GunRadius;

            var isCross =
                cartLocation >= gunBeginLocation &&
                cartLocation <= gunEndLocation;

            if (isCross) {
                var s = string.Format(
                    "{0} cross cart {1}[{2}]", 
                    this.Name, 
                    cart.Name, 
                    cartLocation);
                MyLogManager.Output(s);
            }
            return isCross;
        }

        #region CanUse

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanUse(MaterialAreaList materialAreas) {
            GunWorkStatusEnum gunWorkStatusEnum;
            bool r = CanUse(materialAreas, out gunWorkStatusEnum);

            // set plc gun status
            //
            //this.GunWorkStatus.Status = gunWorkStatusEnum;
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool CanUse(MaterialAreaList materialAreas, out GunWorkStatusEnum gunWorkStatusEnum) {
            if (this.Fault.IsFault) {
                gunWorkStatusEnum = GunWorkStatusEnum.NotWorkWithFault;
                MyLogManager.Output(string.Format("{0} can't by fault", this.Name));
                return false;
            }

            if (this.Mark.IsMarked) {
                gunWorkStatusEnum = GunWorkStatusEnum.NotWorkWithMark;
                MyLogManager.Output(string.Format("{0} can't by mark", this.Name));
                return false;
            }

            if (this.Remote.IsRemote) {
                gunWorkStatusEnum = GunWorkStatusEnum.NotWorkWithRemote;
                MyLogManager.Output(string.Format("{0} can't by remote", this.Name));
                return false;
            }
            if (!this.IsMaterialHeapCanWet(materialAreas)) {
                gunWorkStatusEnum = GunWorkStatusEnum.NotWorkWithMaterialHeap;
                MyLogManager.Output(string.Format("{0} can't by material", this.Name));
                return false;
            }

            if (this.IsCoverCarts()) {
                gunWorkStatusEnum = GunWorkStatusEnum.NotWorkWithCart;
                MyLogManager.Output(string.Format("{0} can't by cart", this.Name));
                return false;
            }

            MyLogManager.Output(string.Format("{0} can use", this.Name));
            gunWorkStatusEnum = GunWorkStatusEnum.Normal;
            return true;
        }
        #endregion //CanUse

        #region Location
        /// <summary>
        /// 
        /// </summary>
        public decimal Location {
            get {
                return this.Define.Location;
            }
        }
        #endregion //Location

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsMaterialHeapCanWet(MaterialAreaList materialAreas) {
            var damAreaName = this.AssociateDamArea.Name;
            var ma = materialAreas.GetByName(damAreaName);

            var gunBegin = Math.Max(this.Location - Config.GunRadius, 0m);
            var gunEnd = this.Location + Config.GunRadius;

            var gunRange = new LineRange(gunBegin, gunEnd);
            var hasCross = ma.MaterialHeapPositions.Any(mhp => {
                var mhpRange = new LineRange(mhp.ReadStartPosition(), mhp.ReadEndPosition());
                var relation = gunRange.DiscernRelation(mhpRange);
                bool isCross = (relation != LineRangeRelation.Disconnection);
                if (isCross) {
                    var s = string.Format("{0} cross {1}[{2}, {3}]",
                        this.Name,
                        ma.ReadStockGroupIdString(),
                        mhp.ReadStartPosition(),
                        mhp.ReadEndPosition());

                    MyLogManager.Output(s);
                }
                return isCross;
            });

            return !hasCross;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CartList GetAssociateCarts() {
            var carts = this.Dam.AssociateCarts;
            if (carts == null || carts.Count == 0) {
                throw new PlException("gun cart is null or empty");
            }
            return carts;
        }
    }
}
