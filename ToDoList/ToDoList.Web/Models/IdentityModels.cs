using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoList.Web.Models
{
    public class ToDoListContext: DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Classification> Classifications { get; set; }
    }

    public class ToDoListDbInitializer : DropCreateDatabaseAlways<ToDoListContext>
    {
        protected override void Seed(ToDoListContext db)
        {
            db.ToDoTasks.Add(new ToDoTask { Description = "Task 1", DueDateTime = DateTime.Now, Status = "WIP" });
            db.ToDoTasks.Add(new ToDoTask { Description = "Task 2", DueDateTime = DateTime.Now, Status = "Suspended" });
            db.ToDoTasks.Add(new ToDoTask { Description = "Task 3", DueDateTime = DateTime.Now, Status = "Done" });
            db.ToDoTasks.Add(new ToDoTask { Description = "Task 4", DueDateTime = DateTime.Now, Status = "WIP" });
            db.SaveChanges();
        }
    }
}