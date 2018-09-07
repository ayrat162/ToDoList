using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDoList.DAL.Entities
{
    class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
