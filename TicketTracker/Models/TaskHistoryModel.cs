using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class TaskHistoryModel
    {
        public string TaskID { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public string value { get; set; }
        public string Action_DT { get; set; }
    }
}