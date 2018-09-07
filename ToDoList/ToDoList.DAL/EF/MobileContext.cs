using System;
using System.Data.Entity;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.EF
{
    public class ToDoListContext: DbContext
    {
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Classification> Classifications { get; set; }
    }
}