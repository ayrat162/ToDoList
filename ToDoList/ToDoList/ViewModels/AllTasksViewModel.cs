using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.ViewModels
{
    public class AllTasksViewModel
    {
        public IEnumerable<ToDoTask> Tasks { get; set; }
    }
}