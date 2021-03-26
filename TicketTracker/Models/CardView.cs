using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class CardView
    {
        public int id { get; set; }
        public string name { get; set; }
        public string progress { get; set; }
        public string description { get; set; }
        public string assigned_to { get; set; }
        public string created_by { get; set; }
        public string created_dt { get; set; }
    }
}