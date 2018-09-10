using System;
using System.Collections.Generic;
using ToDoList.Web.Models;

namespace ToDoList.Web.ViewModels
{
    public class ToDoTaskViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Classification { get; set; }
        public string Status { get; set; }
        public IEnumerable<ClassificationModel> Classifications { get; set; }
    }
}