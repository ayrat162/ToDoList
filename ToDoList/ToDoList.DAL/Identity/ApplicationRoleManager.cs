using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ToDoList.DAL.Entities;

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
