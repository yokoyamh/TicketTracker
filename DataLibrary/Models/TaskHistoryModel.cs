using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TaskHistoryModel
    {
        public int ID { get; set; }
        public string Bug_ID { get; set; }
        public string Action_Type { get; set; }
        public string User_ID { get; set; }
        public string Value { get; set; }
        public string Action_DT { get; set; }
    }
}
