using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        [Required]
        [StringLength(1024)]
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
        public Classification Classification { get; set; }
        public int ClassificationId { get; set; }
    }
}