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
        public ActionResult TaskView(string taskID)
        {
            var BaseData = TaskProcessor.LoadTask(taskID);
            var HistoryData = TaskProcessor.LoadTaskHistory(taskID);
            var CommentData = TaskProcessor.LoadTaskComments(taskID);
            TaskView newTask = new TaskView {
                id = Int32.Parse(taskID),
                name = BaseData[0].Name,
                progress = BaseData[0].Progress,
                description = BaseData[0].Description,
                assigned_to = BaseData[0].Assigned_to,
                created_by = BaseData[0].Created_by,
                created_dt = BaseData[0].Created_DT,
                taskHistories = new List<TaskHistory>(),
                comments = new List<TaskComment>()
            };
            foreach (var row in HistoryData)
            {
                newTask.taskHistories.Add(new TaskHistory
                {
                    TaskID = taskID,
                    action = row.Action_Type,
                    user_id = row.User_ID,
                    value = row.Value
                });
            }
            foreach (var row in CommentData)
            {
                newTask.comments.Add(new TaskComment
                {
                    ID = row.ID,
                    User_ID = row.User_ID,
                    Comment = row.Comment,
                    Created_DT = row.Created_DT
                });
            }
            return View(newTask);
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