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
            context.ToDoTasks.Add(new ToDoTask {Description = "Task 1", DueDateTime = DateTime.Now, Id = 1, Status = "WIP"});
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 2", DueDateTime = DateTime.Now, Id = 2, Status = "WIP" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 3", DueDateTime = DateTime.Now, Id = 3, Status = "WIP" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 4", DueDateTime = DateTime.Now, Id = 4, Status = "WIP" });
            context.ToDoTasks.Add(new ToDoTask { Description = "Task 5", DueDateTime = DateTime.Now, Id = 5, Status = "WIP" });

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
