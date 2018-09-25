using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models.Entities
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }

        public Classification Classification { get; set; }
        public int? ClassificationId { get; set; }

        public ToDoTask ConnectedToDoTask { get; set; }
        public int? ConnectedToDoTaskId { get; set; }

        public Picture Picture { get; set; }
        public int? PictureId { get; set; }

        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public bool IsApproved { get; set; }
    }
}