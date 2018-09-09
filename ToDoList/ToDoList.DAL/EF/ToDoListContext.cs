using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.EF
{
    public class ToDoListContext: IdentityDbContext<ApplicationUser>
    {
        public ToDoListContext(string connectionString) : base(connectionString) { }
        public DbSet<ToDoTask> ToDoTasks { get; set; }
        public DbSet<Classification> Classifications { get; set; }
        public DbSet<ClientProfile> ClientProfiles { get; set; }
        private void FixEfProviderServicesProblem()
        {
            // The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            // for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            // Make sure the provider assembly is available to the running application. 
            // See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
    }
}