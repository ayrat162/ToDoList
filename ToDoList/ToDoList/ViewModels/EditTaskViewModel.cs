using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class EditTaskViewModel
    {
        public IEnumerable<Classification> Classifications { get; set; }
        public IEnumerable<ToDoTask> ToDoTasks { get; set; }
        public ToDoTask ToDoTask { get; set; }
    }
}