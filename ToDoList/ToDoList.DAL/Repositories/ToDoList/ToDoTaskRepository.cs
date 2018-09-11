using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

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
            return db.ToDoTasks;
        }
        public ToDoTask Get(int id)
        {
            return db.ToDoTasks.Find(id);
        }
        public ToDoTask Create(ToDoTask toDoTask)
        {
            var newToDoTask = db.ToDoTasks.Add(toDoTask);



            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges

                db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                string logs = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    logs += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\n",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logs += String.Format("- Property: \"{0}\", Error: \"{1}\"\n",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                File.AppendAllText("exceptionLogs.txt", logs);
                throw;
            }




            return newToDoTask;
        }
        public void Update(ToDoTask toDoTask)
        {
            db.Entry(toDoTask).State = EntityState.Modified;
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
        }
    }
}