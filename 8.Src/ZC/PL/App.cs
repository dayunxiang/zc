using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using Newtonsoft.Json;
using PL.Hardware;

namespace PL {

    public class App {

        #region GetApp
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public App GetApp() {
            return _app;
        } static private App _app = new App();
        #endregion //GetApp

        #region ctor
        /// <summary>
        /// 
        /// </summary>
        private App() {
            var define = InitDefine();

            this.Carts = InitCarts(define);
            this.Dams = InitDams(define, this.Carts);
            this.MaterialAreas= InitMaterialAreas(define);

            this.Gc = define.Gc;
            this.PlTimeRemaining = new PlTimeRemaining(this.Gc.PlTimeRemaining);
            this.Pump = new Pump(this.Gc.CycleEndStopPump);
            //this.Opc = new SimpleOpcServer();
            this.AppController = new AppController(this, this.Gc);
        }
        #endregion //ctor

        #region InitMaterialAreas
        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        private MaterialAreaList InitMaterialAreas(Define define) {
            // todo:
            //
            var r = new MaterialAreaList();
            define.MaterialAreaDefines.ForEach(mad => {
                var ma = mad.Create();
                r.Add(ma);
            });
            //foreach (var materialHeapDefine in define.MaterialHeapDefines) {
            //    var materialHeap = materialHeapDefine.Create();
            //    r.Add(materialHeap);
            //}
            return r;

            throw new NotImplementedException();
        }
        #endregion //InitMaterialAreas

        #region InitCarts
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CartList InitCarts(Define define) {
            var carts = new CartList();
            define.CartDefines.ForEach(d => carts.Add(d.Create()));
            return carts;
        }
        #endregion //InitCarts

        #region InitDams
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DamLinkedList InitDams(Define define, CartList carts) {
            var r = new DamLinkedList();

            LinkedListNode<Dam> lastDamNode = null;
            foreach (var damDefine in define.DamDefines) {
                var dam = damDefine.Create(carts);
                if (lastDamNode == null) {
                    lastDamNode = r.AddFirst(dam);
                } else {
                    lastDamNode = r.AddAfter(lastDamNode, dam);
                }
                dam.DamNode = lastDamNode;
            }
            return r;
        }
        #endregion //InitDams

        #region InitDefine
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Define InitDefine() {
            var json = File.ReadAllText("address.json");
            var define = JsonConvert.DeserializeObject<Define>(json);
            return define;
        }
        #endregion //InitDefine

        #region AppController
        /// <summary>
        /// 
        /// </summary>
        public AppController AppController {
            get;
            private set;
        }
        #endregion //AppController

        #region Dams
        /// <summary>
        /// 
        /// </summary>
        public DamLinkedList Dams {
            get;
            private set;
        }
        #endregion //Dams

        #region Gc
        /// <summary>
        /// 
        /// </summary>
        public Gc Gc {
            get;
            private set;
        }
        #endregion //Gc

        #region Opc
        ///// <summary>
        ///// 
        ///// </summary>
        //public SimpleOpcServer Opc {
        //    get;
        //    private set;
        //}
        #endregion //Opc

        #region Carts
        /// <summary>
        /// 
        /// </summary>
        public CartList Carts {
            get;
            private set;
        }
        #endregion //Carts

        /// <summary>
        /// 
        /// </summary>
        public PlTimeRemaining PlTimeRemaining {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Pump Pump {
            get;
            private set;
        }

        #region MaterialAreas
        /// <summary>
        /// 
        /// </summary>
        public MaterialAreaList MaterialAreas{
            get;
            private set;
        }
        #endregion //MaterialAreas
    }
}
