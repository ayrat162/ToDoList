using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Entities;
using ToDoList.DAL.Interfaces;

namespace ToDoList.DAL.Repositories.Identity
{
    public class RelationshipRepository : IRepository<Relationship>
    {
        private ToDoListContext db;
        public RelationshipRepository(ToDoListContext context)
        {
            this.db = context;
        }
        public IEnumerable<Relationship> GetAll()
        {
            return db.Relationships;
        }
        public Relationship Get(int id)
        {
            return db.Relationships.Find(id);
        }
        public Relationship Create(Relationship classification)
        {
            var newRelationship = db.Relationships.Add(classification);
            db.SaveChanges();
            return newRelationship;
        }
        public void Update(Relationship relationship)
        {
            db.Entry(relationship).State = EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<Relationship> Find(Func<Relationship, Boolean> predicate)
        {
            return db.Relationships.Where(predicate).ToList();
        }
        public void Delete(int id)
        {
            var relationship = db.Relationships.Find(id);
            if (relationship != null)
                db.Relationships.Remove(relationship);
            db.SaveChanges();
        }

    }
}