using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;
using ToDoList.Models.DTO;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers.API
{
    [System.Web.Mvc.Authorize]
    public class TasksApiController : ApiController
    {
        #region services definition
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
        #endregion


        [Route("api/tasks")]
        [HttpGet]
        public IEnumerable<ToDoTaskDTO> GetTasks()
        {
            // TODO: Add Include(Classification) to the context GetToDoTasks
            var classifications = toDoListService.GetClassifications();
            var currentUserId = User.Identity.GetUserId();
            if (Check.IsAdmin(User))
                return toDoListService.GetToDoTasks();
            else
                return toDoListService.GetToDoTasksOf(currentUserId);
        }

        [Route("api/tasks/{id}")]
        [HttpGet]
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

        [Route("api/tasks/create")]
        [HttpPost]
        public IHttpActionResult CreateTask(ToDoTaskDTO toDoTaskDto)
        {
            var currentUserId = User.Identity.GetUserId();
            toDoTaskDto.UserId = currentUserId;
            var newTaskId = toDoListService.AddToDoTask(toDoTaskDto);
            return Created(new Uri(Request.RequestUri + "/" + newTaskId), toDoListService.GetToDoTask(newTaskId));
        }

        [Route("api/tasks/{id}")]
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

        [Route("api/tasks/{id}")]
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
