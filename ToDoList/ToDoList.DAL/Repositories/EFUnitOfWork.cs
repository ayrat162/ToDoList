﻿using System;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories
{
    public class EFUnitOfWork: IUnitOfWork
    {
        private ToDoListContext db;
        private ToDoTaskRepository toDoTaskRepository;
        private ClassificationRepository classificationRepository;

        public EFUnitOfWork()
        {
            db = new ToDoListContext();
        }

        public IRepository<ToDoTask> ToDoTasks
        {
            get
            {
                if(toDoTaskRepository == null)
                    toDoTaskRepository = new ToDoTaskRepository(db);
                return toDoTaskRepository;
            }
        }

        public IRepository<Classification> Classifications
        {
            get
            {
                if (classificationRepository == null)
                    classificationRepository = new ClassificationRepository(db);
                return classificationRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}