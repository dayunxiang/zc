using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PLC;
using Newtonsoft.Json;
using PL.Hardware;

namespace PL
{

    public class App
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static public App GetApp()
        {
            return _app;
        } static private App _app= new App ();

        /// <summary>
        /// 
        /// </summary>
        private App()
        {
            this.Dams = InitDams();
            this.Address2 = InitAddress2();
            this.Opc = new SimpleOpcServer();
            this.AppController = new AppController(this, this.Address2);
            //Lm.D("App()");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Address2 InitAddress2()
        {
            var json = File.ReadAllText("address2.json");
            var a2 = JsonConvert.DeserializeObject<Address2>(json);
            return a2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DamLinkedList InitDams()
        {
            var r = new DamLinkedList();

            var json = File.ReadAllText("address.json");
            var damDefines = JsonConvert.DeserializeObject<List<DamDefine>>(json);

            LinkedListNode<Dam> lastDamNode = null;
            foreach (var damDefine in damDefines)
            {
                var dam = damDefine.Create();
                if(lastDamNode == null)
                {
                    lastDamNode =  r.AddFirst(dam);
                }
                else
                {
                    lastDamNode = r.AddAfter(lastDamNode, dam);
                }
                dam.DamNode = lastDamNode;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public AppController AppController
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public DamLinkedList Dams
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Address2 Address2 { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public SimpleOpcServer Opc { get; private set; }
    }
}
