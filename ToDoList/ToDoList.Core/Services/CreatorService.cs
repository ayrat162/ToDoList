using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Core.Interfaces;
using ToDoList.DAL.Repositories;

namespace ToDoList.Core.Services
{
    public class CreatorService
    {
        public IUserService CreateUserService()
        {
            return new UserService(new EFUnitOfWork());
        }
    }
}
