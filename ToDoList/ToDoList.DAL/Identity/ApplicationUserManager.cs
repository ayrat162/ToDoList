using Microsoft.AspNet.Identity;
using ToDoList.DAL.Entities;

namespace ToDoList.DAL.Identity
{
    public class ApplicationUserManager: UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) 
            : base(store)
        {
        }
    }
}
