using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ToDoList.DAL.EF;
using ToDoList.DAL.Interfaces;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Repositories
{
    public class RelationshipRepository : IRepository<Relationship>
    {
        #region defining context
        private readonly ToDoListContext db;
        public RelationshipRepository(ToDoListContext context)
        {
            this.db = context;
        }
        #endregion

        public IEnumerable<Relationship> GetAll()
        {
            return db.Relationships;
        }

        public Relationship Get(int id)
        {
            return db.Relationships.Find(id);
        }

        public Relationship Create(Relationship relationship)
        {
            var newRelationship = db.Relationships.Add(relationship);
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
            return db.Relationships.Include(r => r.Boss).Where(predicate).ToList();
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