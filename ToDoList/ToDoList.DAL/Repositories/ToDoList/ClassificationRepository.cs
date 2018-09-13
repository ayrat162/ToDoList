using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

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
        public Classification Create(Classification classification)
        {
            var newClassification = db.Classifications.Add(classification);
            db.SaveChanges();
            return newClassification;
        }
        public void Update(Classification classification)
        {
            db.Entry(classification).State = EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<Classification> Find(Func<Classification, Boolean> predicate)
        {
            return db.Classifications.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            var classification = db.Classifications.Find(id);
            if (classification != null)
                db.Classifications.Remove(classification);
            db.SaveChanges();
        }
    }
}