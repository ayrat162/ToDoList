using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoList.Core.Helpers;
using ToDoList.Core.Services;
using ToDoList.Models.DTO;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    [System.Web.Mvc.Authorize]
    public class TasksController : Controller
    {
        #region services definition
        private readonly ToDoListService _toDoListService;
        private readonly UserService _userService;
        public TasksController()
        {
            _toDoListService = new ToDoListService();
            _userService = new UserService();
        }
        #endregion

        [System.Web.Mvc.Route("Tasks")]
        [System.Web.Mvc.Route("")]
        public ActionResult Index()
        {
            //this action works using DataTables JS library and TasksApi controller 
            return View();
        }

        [System.Web.Mvc.Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult View(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            if(Helper.CanEditTask(currentUserId, id)==false)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            var toDoTaskDto = _toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();
            TaskViewModel taskViewModel = new TaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = _toDoListService.GetClassifications(),
                Users = _userService.GetAllUsers(),
                PictureDto = _toDoListService.GetPicture(toDoTaskDto.PictureId)
            };
            return View(taskViewModel);
        }

        [System.Web.Mvc.Route("Tasks/Save")]
        [System.Web.Mvc.HttpPost]
        public ActionResult Save(TaskViewModel taskViewModel, HttpPostedFileBase uploadImage)
        {
            var currentUserId = User.Identity.GetUserId();

            var picture = new PictureDTO();
            if(uploadImage!=null)
                picture.Image = Convert.File2Picture(uploadImage);
            taskViewModel.ToDoTaskDto.CreatedById = currentUserId;
            if (taskViewModel.ToDoTaskDto.Id == 0) // uploading new item
            {
                if (picture.Image != null)
                {
                    var pictureId = _toDoListService.AddPicture(picture);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                _toDoListService.AddToDoTask(taskViewModel.ToDoTaskDto);
            }
            else // updating existing item
            {
                var taskId = taskViewModel.ToDoTaskDto.Id;
                if (Helper.CanEditTask(currentUserId, taskId) == false)
                    throw new HttpResponseException(HttpStatusCode.Forbidden);

                var toDoTaskDto = _toDoListService.GetToDoTask(taskViewModel.ToDoTaskDto.Id);
                if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
                    throw new HttpResponseException(HttpStatusCode.Forbidden);
                if (picture.Image != null)
                {
                    var pictureId = _toDoListService.AddPicture(picture);
                    _toDoListService.DeletePicture(taskViewModel.ToDoTaskDto.PictureId);
                    taskViewModel.ToDoTaskDto.PictureId = pictureId;
                }
                _toDoListService.UpdateToDoTask(taskViewModel.ToDoTaskDto);
            }
            return RedirectToAction("Index", "Tasks");
        }
        
        [System.Web.Mvc.Route("Tasks/New")]
        public ActionResult New()
        {
            var taskViewModel = new TaskViewModel
            {
                Classifications = _toDoListService.GetClassifications(),
                Users = _userService.GetAllUsers()
            };
            return View(taskViewModel);
        }

        [System.Web.Mvc.Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Edit(int id)
        {

            var currentUserId = User.Identity.GetUserId();
            if (Helper.CanEditTask(currentUserId, id) == false)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            var toDoTaskDto = _toDoListService.GetToDoTask(id);
            if (toDoTaskDto == null)
                return HttpNotFound();

            var taskViewModel = new TaskViewModel
            {
                ToDoTaskDto = toDoTaskDto,
                Classifications = _toDoListService.GetClassifications(),
                PictureDto = _toDoListService.GetPicture(toDoTaskDto.PictureId),
                Users = _userService.GetAllUsers()
            };
            return View(taskViewModel);
        }

        [System.Web.Mvc.Route("Tasks/Approve/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Approve(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            if (Helper.CanEditTask(currentUserId, id) == false)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            _toDoListService.ApproveTask(id);

            return RedirectToAction("Index", "Tasks");
        }
    }
}