﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Interfaces;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Repositories
{
    public class PictureRepository : IRepository<Picture>
    {
        private ToDoListContext db;
        public PictureRepository(ToDoListContext context)
        {
            this.db = context;
        }
        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }
        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }
        public Picture Create(Picture picture)
        {
            var newPicture = db.Pictures.Add(picture);
            db.SaveChanges();
            return newPicture;
        }
        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<Picture> Find(Func<Picture, Boolean> predicate)
        {
            return db.Pictures.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            var picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
            db.SaveChanges();
        }
    }
}