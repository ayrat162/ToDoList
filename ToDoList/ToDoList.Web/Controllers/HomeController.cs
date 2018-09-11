using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ToDoListService toDoListService;
        public HomeController(ToDoListService service)
        {
            toDoListService = service;
        }

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            IEnumerable<ToDoTaskDTO> toDoTaskDtos;
            if (Check.IsAdmin(User))
                toDoTaskDtos = toDoListService.GetToDoTasks();
            else
                toDoTaskDtos = toDoListService.GetToDoTasksOf(currentUserId);
            var toDoTasks = new ViewAllTasksViewModel
            {
                ToDoTaskDtos = toDoTaskDtos,
                Classifications = toDoListService.GetClassifications()
            };
            return View(toDoTasks);
        }

        [Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult View(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
            {
                ViewBag.Message = "Access denied. You're not allowed to open this ToDoTask";
                return View("Error");
            }
            TaskViewModel taskViewModel = new TaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = toDoListService.GetClassifications(),
                PictureDto = toDoListService.GetPicture(toDoTaskDto.PictureId)
            };
            return View(taskViewModel);
        }

        [HttpPost]
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
                if (picture.Image != null)
                {
                    var pictureId = toDoListService.AddPicture(picture);
                    toDoListService.DeletePicture(taskViewModel.ToDoTaskDto.PictureId);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                toDoListService.UpdateToDoTask(taskViewModel.ToDoTaskDto);
            }
            return RedirectToAction("Index", "Home");
        }
        
        [Route("Tasks/New")]
        public ActionResult New()
        {
            var taskViewModel = new TaskViewModel
            {
                Classifications = toDoListService.GetClassifications()
            };
            return View(taskViewModel);
        }

        [Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Edit(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
            {
                ViewBag.Message = "Access denied. You're not allowed to open this ToDoTask";
                return View("Error");
            }
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