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
            this.Carts = InitCarts();
            this.Dams = InitDams();

            this.Gc = InitGc();
            this.Opc = new SimpleOpcServer();
            this.AppController = new AppController(this, this.Gc);
        }
        #endregion //ctor

        #region InitCarts
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CartList InitCarts() {
            // todo:
            //
            throw new NotImplementedException();
        }
        #endregion //InitCarts

        #region InitGc
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Gc InitGc() {
            var json = File.ReadAllText("gc.json");
            var a2 = JsonConvert.DeserializeObject<Gc>(json);
            return a2;
        }
        #endregion //InitGc

        #region InitDams
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DamLinkedList InitDams() {
            var r = new DamLinkedList();

            var json = File.ReadAllText("address.json");
            var damDefines = JsonConvert.DeserializeObject<List<DamDefine>>(json);

            LinkedListNode<Dam> lastDamNode = null;
            foreach (var damDefine in damDefines) {
                var dam = damDefine.Create();
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
    }
}
