using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories.Identity
{
    public class ClientManager : IClientManager
    {
        public ToDoListContext Database { get; set; }
        public ClientManager(ToDoListContext db)
        {
            Database = db;
        }
        public void Create (ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
            Database.SaveChanges();
        }
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
