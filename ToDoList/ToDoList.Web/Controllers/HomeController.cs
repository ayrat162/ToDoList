using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ToDoTaskService toDoTaskService;
        public HomeController(ToDoTaskService service)
        {
            toDoTaskService = service;
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            IEnumerable<ToDoTaskDTO> toDoTaskDtos;
            if (Check.IsAdmin(User))
                toDoTaskDtos = toDoTaskService.GetToDoTasks();
            else
                toDoTaskDtos = toDoTaskService.GetToDoTasksOf(currentUserId);
            var toDoTasks = Converter.Dto2ViewModel(toDoTaskDtos);
            return View(toDoTasks);
        }

        [Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult View(int id)
        {
            var toDoTaskDto = toDoTaskService.GetToDoTask(id);
            if (toDoTaskDto == null) return HttpNotFound();
            var toDoTask = Converter.Dto2ViewModel(toDoTaskDto);
            return View(toDoTask);
        }
    }
}