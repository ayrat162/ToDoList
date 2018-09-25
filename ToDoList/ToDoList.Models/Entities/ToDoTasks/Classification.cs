using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entities
{
    public class Classification
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // TODO: User for classification can be defined
    }
}