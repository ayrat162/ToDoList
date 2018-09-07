﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
        public void Create(ToDoTask toDoTask)
        {
            db.ToDoTasks.Add(toDoTask);
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