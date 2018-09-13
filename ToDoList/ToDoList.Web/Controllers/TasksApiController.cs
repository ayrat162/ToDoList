using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;
using System.Web.Mvc;

namespace ToDoList.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksApiController : ApiController
    {
        private ToDoListService toDoListService;
        public TasksApiController(ToDoListService service)
        {
            toDoListService = service;
        }
        ToDoListService service = new ToDoListService();
        public TasksApiController()
        {
            toDoListService = service;
        }

        [System.Web.Mvc.Route("API/Tasks")]
        [System.Web.Mvc.HttpGet]
        public IEnumerable<ToDoTaskDTO> GetTasks()
        {
            var currentUserId = User.Identity.GetUserId();
            if (Check.IsAdmin(User))
                return toDoListService.GetToDoTasks();
            else
                return toDoListService.GetToDoTasksOf(currentUserId);
        }

        [System.Web.Mvc.Route("API/Tasks/{id:regex(\\d):range(0, 1000000)}")]
        [System.Web.Mvc.HttpGet]
        public ToDoTaskDTO GetTask(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            return toDoTaskDto;
        }

        [System.Web.Mvc.Route("API/Tasks/{id:regex(\\d):range(0, 1000000)}")]
        [System.Web.Mvc.HttpPut]
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

        [System.Web.Mvc.Route("API/Tasks/{id:regex(\\d):range(0, 1000000)}")]
        [System.Web.Mvc.HttpDelete]
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

        [System.Web.Mvc.Route("API/Tasks/New")]
        [System.Web.Mvc.HttpPost]
        public IHttpActionResult CreateTask(ToDoTaskDTO toDoTaskDto)
        {
            var currentUserId = User.Identity.GetUserId();
            toDoTaskDto.UserId = currentUserId;
            var newTaskId = toDoListService.AddToDoTask(toDoTaskDto);
            return Created(new Uri(Request.RequestUri + "/" + newTaskId), toDoListService.GetToDoTask(newTaskId));
        }
    }
}
