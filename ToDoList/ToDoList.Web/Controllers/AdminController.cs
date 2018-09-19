using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoList.Core.DTO;
using ToDoList.Core.Services;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    [System.Web.Mvc.Authorize(Roles = Check.AdminRole)]
    [System.Web.Mvc.Authorize]
    public class AdminController : Controller
    {
        private UserService _userService = new UserService();

        [System.Web.Mvc.Route("Users")]
        public ActionResult Index(string id)
        {
            var allUsersViewModel = new AllUsersViewModel();
            var users = _userService.GetAllUsers();
            allUsersViewModel.UserDtos = users;
            return View(allUsersViewModel);
        }


        [System.Web.Mvc.Route("Users/{id}")]
        public ActionResult View(string id)
        {
            var userViewModel = new UserViewModel();
            var user = _userService.GetUser(id);
            if(user==null)
                return HttpNotFound();
            userViewModel.UserDto = user;
            var role = "";
            var roles = _userService.GetRoleForUser(id);
            foreach (var r in roles)
            {
                role += r + " ";
            }
            user.Role = role;
            return View(userViewModel);
        }

        //[System.Web.Mvc.HttpPost]
        //public ActionResult Save(TaskViewModel taskViewModel, HttpPostedFileBase uploadImage)
        //{
        //    var picture = new PictureDTO();
        //    if(uploadImage!=null)
        //        picture.Image = Converter.File2Picture(uploadImage);
        //    var currentUserId = User.Identity.GetUserId();
        //    if (taskViewModel.ToDoTaskDto.Id == 0) // uploading new item
        //    {
        //        if (picture.Image != null)
        //        {
        //            var pictureId = toDoListService.AddPicture(picture);
        //            taskViewModel.ToDoTaskDto.PictureId = pictureId;
        //        }
        //        taskViewModel.ToDoTaskDto.UserId = currentUserId;
        //        toDoListService.AddToDoTask(taskViewModel.ToDoTaskDto);
        //    }
        //    else // uploading edited item data
        //    {
        //        var toDoTaskDto = toDoListService.GetToDoTask(taskViewModel.ToDoTaskDto.Id);
        //        if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
        //            throw new HttpResponseException(HttpStatusCode.Forbidden);
        //        if (picture.Image != null)
        //        {
        //            var pictureId = toDoListService.AddPicture(picture);
        //            toDoListService.DeletePicture(taskViewModel.ToDoTaskDto.PictureId);
        //            taskViewModel.ToDoTaskDto.PictureId = pictureId;
        //        }
        //        toDoListService.UpdateToDoTask(taskViewModel.ToDoTaskDto);
        //    }
        //    return RedirectToAction("Index", "Tasks");
        //}
        
        //[System.Web.Mvc.Route("Tasks/New")]
        //public ActionResult New()
        //{
        //    var taskViewModel = new TaskViewModel
        //    {
        //        Classifications = toDoListService.GetClassifications()
        //    };
        //    return View(taskViewModel);
        //}

        //[System.Web.Mvc.Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        //public ActionResult Edit(int id)
        //{
        //    var currentUserId = User.Identity.GetUserId();
        //    var toDoTaskDto = toDoListService.GetToDoTask(id);
        //    if (toDoTaskDto == null)
        //        return HttpNotFound();
        //    if (!Check.IsAdmin(User) && toDoTaskDto.UserId != currentUserId)
        //        throw new HttpResponseException(HttpStatusCode.Forbidden);
        //    var taskViewModel = new TaskViewModel
        //    {
        //        ToDoTaskDto = toDoTaskDto,
        //        Classifications = toDoListService.GetClassifications(),
        //        PictureDto = toDoListService.GetPicture(toDoTaskDto.PictureId)
        //    };
        //    return View(taskViewModel);
        //}
    }
}