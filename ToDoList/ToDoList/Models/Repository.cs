using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ToDoList.Models
{
    public static class Repository
    {
        private static ApplicationDbContext _context;
        public static void Connect()
        {
            _context = new ApplicationDbContext();
        }
        public static void Disconnect()
        {
            Save();
            _context.Dispose();
        }
        public static void Save()
        {
            _context.SaveChanges();
        }
        public static IEnumerable<ToDoTask> GetTasks()
        {
            return _context.ToDoTasks.Include(t => t.Classification).ToList();
        }
        public static IEnumerable<Classification> GetClassifications()
        {
            return _context.Classifications.ToList();
        }
        public static void RemoveToDoTask(int id)
        {
            _context.ToDoTasks.Remove(_context.ToDoTasks.Single(t => t.Id == id));
            Save();
        }
        public static ToDoTask FindToDoTask(int id)
        {
            return _context.ToDoTasks.SingleOrDefault(task => task.Id == id);
        }
        public static void AddToDoTask(ToDoTask toDoTask)
        {
            _context.ToDoTasks.Add(toDoTask);
            Save();
        }
    }
}