using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TicketTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "CardView", id = "All" }
            );
            routes.MapRoute(
                name: "TaskView",
                url: "Home/TaskView/{taskID}",
                defaults: new { controller = "Home", action = "TaskView", id = "0" }
            );
        }
    }
}
