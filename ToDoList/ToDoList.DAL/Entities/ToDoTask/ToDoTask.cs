using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.DAL.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }

        //public Classification Classification { get; set; }
        //public int? ClassificationId { get; set; }

        //public ToDoTask ConnectedToDoTask { get; set; }
        //public int? ConnectedtoDoTaskId { get; set; }

        //public byte[] Image { get; set; }

        //[Required]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
    }
}