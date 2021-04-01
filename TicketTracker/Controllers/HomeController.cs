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
        public ActionResult CardView(string id)
        {
            var data = TaskProcessor.LoadTasks(id);

            List<CardViewModel> cardAry = new List<CardViewModel>();
            foreach (var row in data)
            {
                cardAry.Add(new CardViewModel
                {
                    id = row.Id,
                    name = row.Name.TrimEnd(' '),
                    progress = row.Progress.TrimEnd(' '),
                    description = row.Description.TrimEnd(' '),
                    assigned_to = row.Assigned_to.TrimEnd(' '),
                    created_by = row.Created_by.TrimEnd(' '),
                    date_created = row.Date_Created.TrimEnd(' ')
                });
            }
            return View(cardAry);
        }
        public ActionResult TaskView(string id)
        {
            TaskViewModel newTask = new TaskViewModel();
            if (String.IsNullOrWhiteSpace(id) || id.TrimEnd(' ').Equals("0"))
            {
                newTask = new TaskViewModel
                {
                    id = 0,
                    name = "Insert Task Name...",
                    progress = "None",
                    description = "Insert Description...",
                    assigned_to = "Insert Assigned User...",
                    created_by = "Insert Your User ID...",
                    date_created = "Insert Date Created...",
                    taskHistories = new List<TaskHistoryModel>(),
                    comments = new List<TaskCommentModel>()
                };
            }
            else
            {
                var BaseData = TaskProcessor.LoadTask(id);
                var HistoryData = TaskProcessor.LoadTaskHistory(id);
                var CommentData = TaskProcessor.LoadTaskComments(id);
                newTask = new TaskViewModel
                {
                    id = Int32.Parse(id),
                    name = BaseData[0].Name.TrimEnd(' '),
                    progress = BaseData[0].Progress.TrimEnd(' '),
                    description = BaseData[0].Description.TrimEnd(' '),
                    assigned_to = BaseData[0].Assigned_to.TrimEnd(' '),
                    created_by = BaseData[0].Created_by.TrimEnd(' '),
                    date_created = BaseData[0].Date_Created.TrimEnd(' '),
                    taskHistories = new List<TaskHistoryModel>(),
                    comments = new List<TaskCommentModel>()
                };
                foreach (var row in HistoryData)
                {
                    TaskHistoryModel newHistory = new TaskHistoryModel
                    {
                        TaskID = id,
                        action = row.Action_Type.TrimEnd(' '),
                        user_id = row.User_ID.TrimEnd(' '),
                        value = row.Value.TrimEnd(' '),
                        Action_DT = row.Action_DT.TrimEnd(' ')
                    };
                    newTask.taskHistories.Add(newHistory);
                }
                foreach (var row in CommentData)
                {
                    newTask.comments.Add(new TaskCommentModel
                    {
                        ID = row.ID,
                        User_ID = row.User_ID.TrimEnd(' '),
                        Comment = row.Comment.TrimEnd(' '),
                        Created_DT = row.Created_DT.TrimEnd(' ')
                    });
                }
            }

            return View(newTask);
        }
        [HttpPost]
        public ActionResult TaskView(TaskViewModel task)
        { 
            if (ModelState.IsValid)
            {
                task.date_created = DateTime.Now.ToString();
                if (task.id == 0)
                {
                    if (task.name.Contains("Insert Task Name..."))
                    {
                        return TaskView("0");
                    }
                    if (task.description.Contains("Insert Description..."))
                    {
                        task.description = "";
                    }
                    if (task.assigned_to.Contains("Insert Assigned User..."))
                    {
                        task.assigned_to = "";
                    }
                    if (task.created_by.Contains("Insert Your User ID..."))
                    {
                        task.created_by = "";
                    }
                    
                    var RowAffected = TaskProcessor.CreateTask(task.name, task.description, task.assigned_to, task.created_by, task.date_created);
                    task.taskHistories = new List<TaskHistoryModel>();
                    task.comments = new List<TaskCommentModel>();
                    return View(task);
                }
                else
                {
                    var RowAffected = TaskProcessor.UpdateTask(task.id.ToString(), task.name, task.description, task.assigned_to, task.created_by, task.date_created);
                    var RowAffected2 = TaskProcessor.AddHistory(task.id.ToString(), "7", "Hayato", "", task.date_created);
                    return TaskView(task.id.ToString());
                }
            }
            return TaskView(task.id.ToString());
        }
        public ActionResult Action(string id)
        {
            string[] identifiers = id.Split('_');
            int identifierCount = identifiers.Count();
            if (identifiers.Count() == 5)
            {
                string view = identifiers[0];
                string viewID = identifiers[1];
                string ID = identifiers[2];
                string action = identifiers[3];
                string actionID = identifiers[4];
                string action_DT = DateTime.Now.ToString();
                if (action.Equals("ProgressEdit"))
                {
                    var RowAffected = TaskProcessor.UpdateProgress(ID, actionID);
                    string[] progressDef = { "Backlog", "Req Gathering", "In Progress", "QA", "Deployed", "User Acceptance" };
                    string HistoryAction = (Array.IndexOf(progressDef, actionID) + 2).ToString();
                    var RowAffected2 = TaskProcessor.AddHistory(ID, HistoryAction, "Hayato", "", action_DT);
                }
                if (action.Equals("Delete"))
                {
                    var RowAffected = TaskProcessor.DeleteTask(ID);
                    var RowAffected2 = TaskProcessor.AddHistory("0", "9", "Hayato", ID, action_DT);
                    return RedirectToAction("CardView", "Home", new { id = "All" });
                }
                if (view.Equals("TaskView"))
                {
                    return RedirectToAction("TaskView", "Home", new { id = viewID });
                }
                else if (view.Equals("CardView"))
                {
                    return RedirectToAction("CardView", "Home", new { id = viewID });
                }
            }

            return RedirectToAction("CardView", "Home");
        }
    }
}