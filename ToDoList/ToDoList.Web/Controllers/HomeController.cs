using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
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
            var toDoTaskDtos = toDoTaskService.GetToDoTasksFor(currentUserId);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTaskViewModel>()).CreateMapper();
            var toDoTasks = mapper.Map<IEnumerable<ToDoTaskDTO>, IEnumerable<ToDoTaskViewModel>>(toDoTaskDtos);
            return View(toDoTasks);
        }

        public ActionResult ViewTask(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var toDoTaskDto = toDoTaskService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<ToDoTaskDTO, ToDoTaskViewModel>()).CreateMapper();
            var toDoTaskViewModel = mapper.Map<ToDoTaskDTO, ToDoTaskViewModel>(toDoTaskDto);
            return View(toDoTaskViewModel);
        }







        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}