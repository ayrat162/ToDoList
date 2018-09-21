using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Models.DTO;

namespace ToDoList.Web.ViewModels
{
    public class TaskViewModel
    {
        public IEnumerable<ClassificationDTO> Classifications { get; set; }
        public ToDoTaskDTO ToDoTaskDto { get; set; }
        public PictureDTO PictureDto { get; set; }
    }
}