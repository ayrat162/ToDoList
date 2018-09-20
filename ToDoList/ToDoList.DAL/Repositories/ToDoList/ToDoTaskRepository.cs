using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Interfaces;
using System.Data.Entity.Infrastructure;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Repositories
{
    public class ToDoTaskRepository: IRepository<ToDoTask>
    {
        private ToDoListContext db;
        public ToDoTaskRepository(ToDoListContext context)
        {
            this.db = context;
        }
        public IEnumerable<ToDoTask> GetAll()
        {
            return db.ToDoTasks.Include(t=>t.User);
        }
        public ToDoTask Get(int id)
        {
            return db.ToDoTasks.Include(t=>t.User).SingleOrDefault(t=>t.Id==id);
        }
        public ToDoTask Create(ToDoTask toDoTask)
        {
            var newToDoTask = db.ToDoTasks.Add(toDoTask);
            db.SaveChanges();
            return newToDoTask;
        }
        public void Update(ToDoTask toDoTask)
        {
            db.Entry(toDoTask).State = EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<ToDoTask> Find(Func<ToDoTask, Boolean> predicate)
        {
            return db.ToDoTasks.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            var toDoTask = db.ToDoTasks.Find(id);
            if (toDoTask != null)
                db.ToDoTasks.Remove(toDoTask);
            db.SaveChanges();
        }
    }
}