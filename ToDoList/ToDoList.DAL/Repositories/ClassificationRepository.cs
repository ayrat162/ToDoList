using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;
using ToDoList.Web.Models;

namespace ToDoList.DAL.Repositories
{
    public class ClassificationRepository: IRepository<Classification>
    {
        private ToDoListContext db;
        public ClassificationRepository(ToDoListContext context)
        {
            this.db = context;
        }
        public IEnumerable<Classification> GetAll()
        {
            return db.Classifications;
        }
        public Classification Get(int id)
        {
            return db.Classifications.Find(id);
        }
        public void Create(Classification Classification)
        {
            db.Classifications.Add(Classification);
        }
        public void Update(Classification Classification)
        {
            db.Entry(Classification).State = EntityState.Modified;
        }
        public IEnumerable<Classification> Find(Func<Classification, Boolean> predicate)
        {
            return db.Classifications.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            var Classification = db.Classifications.Find(id);
            if (Classification != null)
                db.Classifications.Remove(Classification);
        }
    }
}