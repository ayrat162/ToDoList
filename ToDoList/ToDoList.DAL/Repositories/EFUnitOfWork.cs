using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using ToDoList.DAL.EF;
using ToDoList.DAL.Identity;
using ToDoList.DAL.Interfaces;
using ToDoList.DAL.Repositories.Identity;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Repositories
{
    public class EFUnitOfWork
    {
        private ToDoListContext db;
        private ToDoTaskRepository toDoTaskRepository;
        private ClassificationRepository classificationRepository;
        private PictureRepository pictureRepository;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private ClientManager clientManager;
        private RelationshipRepository relationshipRepository;

        public EFUnitOfWork()
        {
            db = new ToDoListContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }

        public IRepository<ToDoTask> ToDoTasks
        {
            get
            {
                if(toDoTaskRepository == null)
                    toDoTaskRepository = new ToDoTaskRepository(db);
                return toDoTaskRepository;
            }
        }
        public IRepository<Classification> Classifications
        {
            get
            {
                if (classificationRepository == null)
                    classificationRepository = new ClassificationRepository(db);
                return classificationRepository;
            }
        }
        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }
        public IRepository<Relationship> Relationships
        {
            get
            {
                if (relationshipRepository == null)
                    relationshipRepository = new RelationshipRepository(db);
                return relationshipRepository;
            }
        }


        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public ClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Save()
        {
            db.SaveChanges();
        }


        #region dispose
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}