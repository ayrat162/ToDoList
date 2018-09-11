using System;
using System.Collections.Generic;
using ToDoList.Core.DTO;
using ToDoList.Web.Models;

namespace ToDoList.Web.ViewModels
{
    public class ViewTaskViewModel
    {
        public ToDoTaskDTO ToDoTaskDto { get; set; }
        public IEnumerable<ClassificationDTO> Classifications { get; set; }
        public PictureDTO PictureDto { get; set; }

    }
}