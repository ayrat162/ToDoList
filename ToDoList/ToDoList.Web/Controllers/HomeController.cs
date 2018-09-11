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
            var toDoTasks = new ViewAllTasksViewModel
            {
                ToDoTaskDtos = toDoTaskDtos,
                Classifications = toDoTaskService.GetClassifications()
            };
            return View(toDoTasks);
        }

        [Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult View(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var toDoTaskDto = toDoTaskService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
            {
                ViewBag.Message = "Access denied. You're not allowed to view this To Do Task";
                return View("Error");
            }
            ViewTaskViewModel viewModel = new ViewTaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = toDoTaskService.GetClassifications(),
                PictureDto = toDoTaskService.GetPicture(toDoTaskDto.PictureId)
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(EditTaskViewModel taskViewModel, HttpPostedFileBase uploadImage)
        {
            var picture = new PictureDTO();
            if(uploadImage!=null)
                picture.Image = Converter.File2Picture(uploadImage);
            var currentUserId = User.Identity.GetUserId();
            if (taskViewModel.ToDoTaskDto.Id == 0) // uploading new item
            {
                if (picture.Image != null)
                {
                    var pictureId = toDoTaskService.AddPicture(picture);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                taskViewModel.ToDoTaskDto.UserId = currentUserId;
                toDoTaskService.AddToDoTask(taskViewModel.ToDoTaskDto);
            }
            else // uploading edited item data
            {
                if (picture.Image != null)
                {
                    var pictureId = toDoTaskService.AddPicture(picture);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                toDoTaskService.UpdateToDoTask(taskViewModel.ToDoTaskDto);
            }
            return RedirectToAction("Index", "Home");
        }
        
        [Route("Tasks/New")]
        public ActionResult New()
        {
            var viewModel = new NewTaskViewModel
            {
                Classifications = toDoTaskService.GetClassifications()
            };
            return View(viewModel);
        }


    }
}