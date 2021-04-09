using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketTracker.Models
{
    public class CardViewModel
    {
        public string view { get; set; }
        public List<CardModel> cardAry { get; set; }
        public string[] progressDef = { "Backlog", "Req Gathering", "In Progress", "QA", "Deployed", "User Acceptance" };
        public string[] progressClass = { "btn-light", "btn-secondary", "btn-info", "btn-warning", "btn-primary", "btn-success" };
        public string[] priorityDef = { "High", "Medium", "Low", "None" };
        public string[] priorityClass = { "btn-danger", "btn-warning", "btn-success", "btn-secondary" };
    }
}