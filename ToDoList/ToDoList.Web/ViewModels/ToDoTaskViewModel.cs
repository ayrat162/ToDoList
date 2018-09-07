using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Web.ViewModels
{
    public class ToDoTaskViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
    }
}