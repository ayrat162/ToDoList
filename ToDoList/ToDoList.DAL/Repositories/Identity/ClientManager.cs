﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.DAL.EF;
using ToDoList.DAL.Interfaces;
using ToDoList.Models.DTO;
using ToDoList.Models.Entities;

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


        public IEnumerable<ClientProfile> GetAllUsers()
        {
            return Database.ClientProfiles.ToList();
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
