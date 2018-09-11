using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Core.DTO;

namespace ToDoList.Web.ViewModels
{
    public class EditTaskViewModel
    {
        public IEnumerable<ClassificationDTO> Classifications { get; set; }
        public ToDoTaskDTO ToDoTaskDto { get; set; }
        public PictureDTO PictureDto { get; set; }
    }
}