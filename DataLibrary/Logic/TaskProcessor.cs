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
        public static int CreateTask(string name, int progress, string description, 
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
            string sql = @"INSERT into dbo.Tasks (Name, Progress, Description, Assigned_To, Created_By, Date_Created)
                           VALUES (@Name, @Progress, @Description, @Assigned_To, @Created_by, @Created_DT);";
            return sqlDataAccess.SaveData(sql, data);
        }
        public static List<TaskModel> LoadTasks(string Progress)
        {
            string sql = "";
            if (Progress.ToUpper().Equals("All"))
            {
                sql = @"SELECT Name, Progress, Description, Assigned_To, Created_By, Date_Created
                            FROM dbo.Tasks;";
            }
            else
            {
                sql = @"SELECT Name, Progress, Description, Assigned_To, Created_By, Date_Created
                            FROM dbo.Tasks
                            WHERE Progress = '" + Progress + @"';";
            }
            return sqlDataAccess.LoadData<TaskModel>(sql);
        }
    }
}