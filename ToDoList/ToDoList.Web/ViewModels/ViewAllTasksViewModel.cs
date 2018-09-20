using System;
using System.Collections.Generic;
using ToDoList.Models.DTO;
using ToDoList.Web.Models;

namespace ToDoList.Web.ViewModels
{
    public class ViewAllTasksViewModel
    {
        public IEnumerable<ToDoTaskDTO> ToDoTaskDtos { get; set; }
        public IEnumerable<ClassificationDTO> Classifications { get; set; }

    }
}