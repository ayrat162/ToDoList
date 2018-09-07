using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Web.Models;

namespace ToDoList.Web.ViewModels
{
    public class ToDoTaskViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DueDateTime { get; set; }
        public string Status { get; set; }
        public Classification Classification { get; set; }
        public ToDoTask ConnectedToDoTask { get; set; }
    }
}