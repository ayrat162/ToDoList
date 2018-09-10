using System;

namespace ToDoList.Core.DTO
{
    public class ToDoTaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
        public int? ClassificationId { get; set; }
        public int? ConnectedtoDoTaskId { get; set; }
        public int? ImageId { get; set; }
        public string UserId { get; set; }
    }
}