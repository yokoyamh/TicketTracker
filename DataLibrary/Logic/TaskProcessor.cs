using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;

namespace DataLibrary.Logic
{
    public static class TaskProcessor
    {
        public static int CreateTask(string name, string progress, string description, 
                                     string assigned_to, string created_by, string created_dt)
        {
            TaskModel data = new TaskModel
            {
                Name = name,
                Progress = progress,
                Description = description,
                Assigned_to = assigned_to,
                Created_by = created_by,
                Created_DT = created_dt
            };
            string sql = @"INSERT INTO dbo.Tasks (Name, Progress, Description, Assigned_To, Created_By, Date_Created)
                           VALUES (@Name, @Progress, @Description, @Assigned_To, @Created_by, @Created_DT);";//TODO: Fix progress
            return sqlDataAccess.SaveData(sql, data);
        }
        public static List<TaskModel> LoadTasks(string Progress)
        {
            string sql = "";
            if (Progress.ToUpper().Equals("ALL"))
            {
                sql = @"SELECT task.Name, progress.Name as Progress, task.Description, task.Assigned_To, task.Created_By, task.Date_Created
                            FROM dbo.Tasks as task
                            INNER JOIN dbo.Progress as progress ON 
                                task.Progress = progress.ID;";
            }
            else
            {
                sql = @"SELECT task.Name, progress.Name as Progress, task.Description, task.Assigned_To, task.Created_By, task.Date_Created
                            FROM dbo.Tasks as task
                            INNER JOIN dbo.Progress as progress ON 
                                task.Progress = progress.ID
                            WHERE progress.Name = '" + Progress + @"';";
            }
            return sqlDataAccess.LoadData<TaskModel>(sql);
        }
        public static List<TaskModel> LoadTask(string TaskID)
        {
            string sql = @"SELECT TOP(1) task.Name, progress.Name as Progress, task.Description, task.Assigned_To, task.Created_By, task.Date_Created
                            FROM dbo.Tasks as task
                            INNER JOIN dbo.Progress as progress ON 
                                task.Progress = progress.ID;";
            return sqlDataAccess.LoadData<TaskModel>(sql);
        }
        public static List<TaskHistory> LoadTaskHistory(string TaskID)
        {
            string sql = @"SELECT History.ID, History.Bug_ID, actionType.Name as ActionType, History.User_ID, History.Value
                            FROM dbo.TaskHistory as History
                            INNER JOIN dbo.Action_Type as actionType ON
                                actionType.ID = History.Action_Type
                            WHERE Bug_ID = " + TaskID + ";";
            return sqlDataAccess.LoadData<TaskHistory>(sql);
        }
        public static List<TaskComment> LoadTaskComments(string TaskID)
        {
            string sql = @"SELECT ID, Bug_ID, User_ID, Comment, Created_DT
                            FROM dbo.TaskComment as Comment
                            WHERE Bug_ID = " + TaskID + ";";
            return sqlDataAccess.LoadData<TaskComment>(sql);
        }
    }
}