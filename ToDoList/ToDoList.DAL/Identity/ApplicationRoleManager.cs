using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ToDoList.Models.Entities;

namespace ToDoList.DAL.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(RoleStore<ApplicationRole> store)
            : base(store)
        {
        }
    }
}
