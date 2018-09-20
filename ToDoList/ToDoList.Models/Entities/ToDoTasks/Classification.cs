using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entities
{
    public class Classification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //[Required]
        //public ApplicationUser User { get; set; }
        //public string UserId { get; set; }
    }
}