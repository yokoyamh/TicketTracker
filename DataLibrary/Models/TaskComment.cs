using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TaskComment
    {
        public int ID { get; set; }
        public string Bug_ID { get; set; }
        public string User_ID { get; set; }
        public string Comment { get; set; }
        public string Created_DT { get; set; }
}
}
