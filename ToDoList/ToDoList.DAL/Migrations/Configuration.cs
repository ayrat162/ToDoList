using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDoList.DAL.EF.ToDoListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ToDoList.DAL.EF.ToDoListContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 1", DueDateTime = DateTime.Now, Status = "WIP", UserId= "3e5e6543-90dc-48d7-94b0-eff9e189578a" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 2", DueDateTime = DateTime.Now, Status = "WIP", UserId = "3e5e6543-90dc-48d7-94b0-eff9e189578a" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 3", DueDateTime = DateTime.Now, Status = "WIP", UserId = "3e5e6543-90dc-48d7-94b0-eff9e189578a" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 4", DueDateTime = DateTime.Now, Status = "WIP", UserId = "3e5e6543-90dc-48d7-94b0-eff9e189578a" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 5", DueDateTime = DateTime.Now, Status = "WIP", UserId = "3e5e6543-90dc-48d7-94b0-eff9e189578a" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
