using System;
using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace DevWebAPI.Models
{
    public class TaskRepository
    {
        private IDbConnection db;

        public TaskRepository()
        {
            db = new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public Task AddTask(Task model)
        {
            var id = this.db.Query<int>(@"insert into tasks (Title) values (@Title); select CAST(LAST_INSERT_ID() AS unsigned);", model).Single();
            model.Id = id;

            return model;
        }

        public List<Task> GetTasks()
        {
            return this.db.Query<Task>(@"select * from tasks order by Id").ToList();
        }

        public Task GetTask(int id)
        {
            return this.db.Query<Task>(@"select * from tasks where id = @Id", new { Id = id }).SingleOrDefault();
        }

        public Task UpdateTask(Task model)
        {
            string sql = @"update tasks set Title=@Title where Id=@Id";
            this.db.Execute(sql, model);
            return GetTask(model.Id);
        }

        public void RemoveTask(int id)
        {
            string sql = @"delete from tasks where Id=@Id";
            this.db.Execute(sql, new { Id = id });
        }
    }
}