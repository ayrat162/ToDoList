using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDoList.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ClientProfile ClientProfile { get; set; }
    }
}
