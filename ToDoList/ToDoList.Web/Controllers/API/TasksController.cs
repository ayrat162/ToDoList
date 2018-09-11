using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;

namespace ToDoList.Web.Controllers.API
{
    public class TasksController : ApiController
    {
        private ToDoListService toDoListService;
        public TasksController(ToDoListService service)
        {
            toDoListService = service;
        }
        public IEnumerable<ToDoTaskDTO> GetTasks()
        {
            return toDoListService.GetToDoTasks();
        }

        public ToDoTaskDTO GetTask(int id)
        {
            var toDoTask = toDoListService.GetToDoTask(id);
            if (toDoTask == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return toDoTask;
        }

        [HttpPost]
        public ToDoTaskDTO CreateTask(ToDoTaskDTO toDoTaskDto)
        {
            if(ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var newTaskId = toDoListService.AddToDoTask(toDoTaskDto);
            return toDoListService.GetToDoTask(newTaskId);
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

        public void DeleteTask(int id)
        {

        }
    }
}
