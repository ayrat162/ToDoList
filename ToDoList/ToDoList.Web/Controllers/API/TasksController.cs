using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers.API
{
    [System.Web.Mvc.Authorize]
    public class TasksController : ApiController
    {
        private ToDoListService toDoListService;
        public TasksController(ToDoListService service)
        {
            toDoListService = service;
        }
        ToDoListService service = new ToDoListService();
        public TasksController()
        {
            toDoListService = service;
        }

        public ViewAllTasksViewModel GetTasks()
        {
            var currentUserId = User.Identity.GetUserId();
            var tasksViewModel= new ViewAllTasksViewModel();
            tasksViewModel.Classifications = toDoListService.GetClassifications();
            if (Check.IsAdmin(User))
                tasksViewModel.ToDoTaskDtos = toDoListService.GetToDoTasks();
            else
                tasksViewModel.ToDoTaskDtos = toDoListService.GetToDoTasksOf(currentUserId);
            return tasksViewModel;
        }
        public ToDoTaskDTO GetTask(int id)
        {
            var toDoTask = toDoListService.GetToDoTask(id);
            if (toDoTask == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return toDoTask;
        }
        [HttpPost]
        public IHttpActionResult CreateTask(ToDoTaskDTO toDoTaskDto)
        {
            //if(ModelState.IsValid)
            //    return BadRequest();
            var newTaskId = toDoListService.AddToDoTask(toDoTaskDto);
            return Created(new Uri(Request.RequestUri + "/" + newTaskId), toDoListService.GetToDoTask(newTaskId));
        }
        [HttpPut]
        public ToDoTaskDTO UpdateTask(int id, ToDoTaskDTO toDoTaskDto)
        {
            if (ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var currentUserId = User.Identity.GetUserId();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            toDoListService.UpdateToDoTask(toDoTaskDto);
            return toDoListService.GetToDoTask(id);
        }
        [HttpDelete]
        public void DeleteTask(int id)
        {
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if(toDoTaskDto==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            var currentUserId = User.Identity.GetUserId();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            toDoListService.DeleteToDoTask(id);
        }
    }
}
