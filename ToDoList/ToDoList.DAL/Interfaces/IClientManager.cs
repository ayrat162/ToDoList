using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Interfaces
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
    }
}
