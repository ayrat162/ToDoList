using System;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<ToDoTask> ToDoTasks { get; }
        IRepository<Classification> Classifications { get; }
        void Save();
    }
}