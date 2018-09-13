using System;

namespace ToDoList.Core.DTO
{
    public class ToDoTaskDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
        public string ClassificationName { get; set; }
        public int? ClassificationId { get; set; }
        public string ConnectedTaskName { get; set; }
        public int? ConnectedToDoTaskId { get; set; }
        public int? PictureId { get; set; }
        public string UserUserName { get; set; }
        public string UserId { get; set; }
    }
}