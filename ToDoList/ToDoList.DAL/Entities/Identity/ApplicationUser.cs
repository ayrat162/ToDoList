using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDoList.DAL.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
