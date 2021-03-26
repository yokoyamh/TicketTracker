using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;
using System.Configuration;
using System.Data;
using DataLibrary.Logic;

namespace TicketTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult CardView(string category)
        {
            var data = TaskProcessor.LoadTasks(category);

            List<CardView> cardAry = new List<CardView>();
            foreach (var row in data)
            {
                cardAry.Add(new CardView
                {
                    id = row.Id,
                    name = row.Name,
                    progress = row.Progress,
                    description = row.Description,
                    assigned_to = row.Assigned_to,
                    created_by = row.Created_by,
                    created_dt = row.Created_DT
                });
            }
            return View(cardAry);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}