using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.EF
{
    public class ToDoListContext: IdentityDbContext<ApplicationUser>
    {
        public ToDoListContext() : base() { }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        private void FixEfProviderServicesProblem()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}