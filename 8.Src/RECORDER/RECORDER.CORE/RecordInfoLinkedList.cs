using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace RECORDER.CORE {

    public class RecordInfoLinkedList : LinkedList<RecordInfo> {

        public RecordInfoLinkedList() {
        }
    }

    public class RecordInfoLinkedListManager {

        static public RecordInfoLinkedListManager Instance = new RecordInfoLinkedListManager();


        /// <summary>
        /// 
        /// </summary>
        private RecordInfoLinkedListManager() {
            var files = Directory.GetFiles(@".\jsons", "*.json");
            var ris = new RecordInfoLinkedList();

            foreach (var file in files) {
                ris.AddLast(CreateRecordInfo(file));
            }

            this.RecordInfoLinkedList = ris;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private RecordInfo CreateRecordInfo(string file) {
            //var record = Record.FromJsonFile(file);

            var ri = new RecordInfo();
            ri.Name = file;
            ri.StartDateTime = DateTime.MinValue;//record.StartDateTime;
            ri.EndDateTime = DateTime.MinValue;
            ri.Size = (int)new FileInfo(file).Length;
            return ri;
        }

        public RecordInfoLinkedList RecordInfoLinkedList { get; set; }
    }
}
