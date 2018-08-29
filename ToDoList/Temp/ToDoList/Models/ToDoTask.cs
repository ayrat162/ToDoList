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

        [Display(Name = "Due Date & Time")]
        public DateTime DueDateTime { get; set; }

        public string Status { get; set; }

        public Classification Classification { get; set; }

        [Display(Name = "Classification")]
        public int ClassificationId { get; set; }
    }
}