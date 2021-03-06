﻿using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoList.Core.Services;
using ToDoList.Models.DTO;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksController : Controller
    {
        private ToDoListService toDoListService;
<<<<<<< HEAD:ToDoList/ToDoList.Web/Controllers/TasksController.cs
        public TasksController(ToDoListService service)
=======
        //public TasksController(ToDoListService service)
        //{
        //    toDoListService = service;
        //}
        public TasksController()
>>>>>>> dev:ToDoList/ToDoList.Web/Controllers/TasksController.cs
        {
            toDoListService = new ToDoListService();
        }
        public ActionResult Index()
        {
            return View();
        }
        

        [System.Web.Mvc.Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult View(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            TaskViewModel taskViewModel = new TaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = toDoListService.GetClassifications(),
                PictureDto = toDoListService.GetPicture(toDoTaskDto.PictureId)
            };
            return View(taskViewModel);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Save(TaskViewModel taskViewModel, HttpPostedFileBase uploadImage)
        {
            var picture = new PictureDTO();
            if(uploadImage!=null)
                picture.Image = Converter.File2Picture(uploadImage);
            var currentUserId = User.Identity.GetUserId();
            if (taskViewModel.ToDoTaskDto.Id == 0) // uploading new item
            {
                if (picture.Image != null)
                {
                    var pictureId = toDoListService.AddPicture(picture);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                taskViewModel.ToDoTaskDto.UserId = currentUserId;
                toDoListService.AddToDoTask(taskViewModel.ToDoTaskDto);
            }
            else // uploading edited item data
            {
                var toDoTaskDto = toDoListService.GetToDoTask(taskViewModel.ToDoTaskDto.Id);
                if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                if (picture.Image != null)
                {
                    var pictureId = toDoListService.AddPicture(picture);
                    toDoListService.DeletePicture(taskViewModel.ToDoTaskDto.PictureId);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                toDoListService.UpdateToDoTask(taskViewModel.ToDoTaskDto);
            }
            return RedirectToAction("Index", "Tasks");
        }
        
        [System.Web.Mvc.Route("Tasks/New")]
        public ActionResult New()
        {
            var taskViewModel = new TaskViewModel
            {
                Classifications = toDoListService.GetClassifications()
            };
            return View(taskViewModel);
        }

        [System.Web.Mvc.Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Edit(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            var taskViewModel = new TaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = toDoListService.GetClassifications(),
                PictureDto = toDoListService.GetPicture(toDoTaskDto.PictureId)
            };
            return View(taskViewModel);
        }
    }
}