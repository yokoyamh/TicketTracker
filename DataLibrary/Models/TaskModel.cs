using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Priority { get; set; }
        public string Progress { get; set; }
        public string Description { get; set; }
        public string Assigned_to { get; set; }
        public string Created_by { get; set; }
        public string Date_Created { get; set; }
    }
}
