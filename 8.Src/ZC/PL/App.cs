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
            this.AppController = new AppController();
            this.Dams = InitDams();
            //Lm.D("App()");
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
    }
}
