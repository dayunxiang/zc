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
            this.MaterialHeaps = InitMaterialHeaps(define);

            this.Gc = define.Gc;
            this.Opc = new SimpleOpcServer();
            this.AppController = new AppController(this, this.Gc);
        }
        #endregion //ctor

        #region InitMaterialHeaps
        /// <summary>
        /// 
        /// </summary>
        /// <param name="define"></param>
        /// <returns></returns>
        private MaterialHeapList InitMaterialHeaps(Define define) {
            var r = new MaterialHeapList();
            foreach (var materialHeapDefine in define.MaterialHeapDefines) {
                var materialHeap = materialHeapDefine.Create();
                r.Add(materialHeap);
            }
            return r;
        }
        #endregion //InitMaterialHeaps

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
        private Define InitDefine() {
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
        /// <summary>
        /// 
        /// </summary>
        public SimpleOpcServer Opc {
            get;
            private set;
        }
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

        #region MaterialHeaps
        /// <summary>
        /// 
        /// </summary>
        public MaterialHeapList MaterialHeaps {
            get;
            private set;
        }
        #endregion //MaterialHeaps
    }
}
