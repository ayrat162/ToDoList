using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

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
            return db.Pictures.Add(picture);
        }
        public void Update(Picture picture)
        {
            db.Entry(picture).State = EntityState.Modified;
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
        }
    }
}