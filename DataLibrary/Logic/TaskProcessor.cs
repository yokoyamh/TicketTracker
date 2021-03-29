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
                                     string assigned_to, string created_by, string date_created)
        {
            TaskModel data = new TaskModel
            {
                Name = name,
                Progress = progress,
                Description = description,
                Assigned_to = assigned_to,
                Created_by = created_by,
                Date_Created = date_created
            };
            string sql = @"INSERT INTO dbo.Tasks (Name, Progress, Description, Assigned_To, Created_By, Date_Created)
                           SELECT @Name, p.ID, @Description, @Assigned_To, @Created_by, @Created_DT
                                FROM dbo.Progress as p
                                WHERE p.Name = @Progress;";
            return sqlDataAccess.SaveData(sql, data);
        }
        public static int UpdateTask(string id, string name, string progress, string description,
                                     string assigned_to, string created_by, string date_created)
        {
            TaskModel data = new TaskModel
            {
                Id = Int32.Parse(id),
                Name = name,
                Progress = progress,
                Description = description,
                Assigned_to = assigned_to,
                Created_by = created_by,
                Date_Created = date_created
            };
            string sql = @"UPDATE dbo.Tasks 
                           SET Name = @Name, 
                                Progress = Pr.ID,
                                Description = @Description, 
                                Assigned_To = @Assigned_to, 
                                Created_By = @Created_by,
                                Date_Created = @Date_Created
                           FROM dbo.Progress as Pr
                           WHERE Tasks.ID = @Id AND Pr.Name = @Progress;";
            return sqlDataAccess.SaveData(sql, data);
        }

        public static List<TaskModel> LoadTasks(string Progress)
        {
            string sql = "";
            if (Progress.ToUpper().Equals("ALL"))
            {
                sql = @"SELECT task.ID, task.Name, progress.Name as Progress, task.Description, task.Assigned_To, task.Created_By, task.Date_Created
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
                                task.Progress = progress.ID
                            WHERE task.ID = " + TaskID + @";";
            return sqlDataAccess.LoadData<TaskModel>(sql);
        }
        public static List<TaskHistoryModel> LoadTaskHistory(string TaskID)
        {
            string sql = @"SELECT History.ID, History.Bug_ID, actionType.Name as ActionType, History.User_ID, History.Value, History.Action_DT
                            FROM dbo.TaskHistory as History
                            INNER JOIN dbo.Action_Type as actionType ON
                                actionType.ID = History.Action_Type
                            WHERE Bug_ID = " + TaskID + ";";
            return sqlDataAccess.LoadData<TaskHistoryModel>(sql);
        }
        public static List<TaskCommentModel> LoadTaskComments(string TaskID)
        {
            string sql = @"SELECT ID, Bug_ID, User_ID, Comment, Created_DT
                            FROM dbo.TaskComment as Comment
                            WHERE Bug_ID = " + TaskID + ";";
            return sqlDataAccess.LoadData<TaskCommentModel>(sql);
        }
    }
}