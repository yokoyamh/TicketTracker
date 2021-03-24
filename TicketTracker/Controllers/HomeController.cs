using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;
using System.Configuration;
using System.Data;
using DataLibrary;

namespace TicketTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult CardView(string category)
        {
            var data = LoadTasks(category);
            List<CardView> cardAry = new List<CardView>();
            CardView newCard = new CardView() { id = 0, name = "teste", description = "test", progress = category };
            return View(newCard);
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