using System.Linq;
using System.Net;
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
    [System.Web.Mvc.Authorize(Roles = Check.Admin)]
    public class UsersController : Controller
    {
        private UserService userService;
        public UsersController(UserService service)
        {
            userService = service;
        }
        public UsersController()
        {
            userService = new UserService();
        }

        [System.Web.Mvc.Route("Users")]
        public ActionResult AllUsers()
        {
            return View();
        }

        [System.Web.Mvc.Route("Users/{id}")]
        public ActionResult SingleUser(string id)
        {
            var user = userService.GetUser(id);
            if(user==null) return HttpNotFound();
            return View(user);
        }

        [System.Web.Mvc.Route("Users/Edit/{id}")]
        public ActionResult EditUser(string id)
        {
            var user = userService.GetUser(id);
            var roles = userService.GetRoles();
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role,
                RoleId = user.RoleId,
                Roles = roles,
                Email = user.Email
            };
            return View(userViewModel);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Users/Save")]
        public ActionResult SaveUser(UserAndRoleDTO userViewModel)
        {
            userService.UpdateUser(userViewModel);
            return RedirectToAction("AllUsers", "Users");
        }
    }
}