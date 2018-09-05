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
        static public App GetApp()
        {
            return _app;
        } static private App _app= new App ();

        /// <summary>
        /// 
        /// </summary>
        private App()
        {
            this.Controller = new AppController();
            this.Dams = InitDams();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DamLinkedList InitDams()
        {
            var r = new DamLinkedList();

            var json = File.ReadAllText("1.json");
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
                    r.AddAfter(lastDamNode, dam);
                }
                dam.DamNode = lastDamNode;
            }
            return r;
        }

        /// <summary>
        /// 
        /// </summary>
        public AppController Controller
        {
            get;
            private set;
        }

        public DamLinkedList Dams
        {
            get;
            private set;
        }
    }
}
