﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Interfaces;
using ToDoList.Models.Entities;

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
        public void Update(Classification Classification)
        {
            db.Entry(Classification).State = EntityState.Modified;
            db.SaveChanges();
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
            db.SaveChanges();
        }
    }
}