using System;
using System.Threading.Tasks;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Identity;

namespace ToDoList.DAL.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<ToDoTask> ToDoTasks { get; }
        IRepository<Classification> Classifications { get; }
        IRepository<Picture> Pictures { get;  }
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        void Save();
        Task SaveAsync();
    }
}