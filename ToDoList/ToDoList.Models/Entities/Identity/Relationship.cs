using Microsoft.AspNet.Identity.EntityFramework;

namespace ToDoList.Models.Entities
{
    public class Relationship
    {
        public int Id { get; set; }
        public ApplicationUser Boss { get; set; }
        public string BossId { get; set; }

        public ApplicationUser Child { get; set; }
        public string ChildId { get; set; }
    }
}
