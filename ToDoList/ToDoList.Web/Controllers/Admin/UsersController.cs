﻿using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ToDoList.Core.Services;
using ToDoList.Models;
using ToDoList.Models.DTO;
using ToDoList.Web.Helpers;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    [System.Web.Mvc.Authorize(Roles = RoleNames.Admin)]
    public class UsersController : Controller
    {
        #region service definitions
        private UserService userService;
        public UsersController(UserService service)
        {
            userService = service;
        }
        public UsersController()
        {
            userService = new UserService();
        }
        #endregion

        [System.Web.Mvc.Route("Users")]
        public ActionResult AllUsers()
        {
            return View();
        }

        [System.Web.Mvc.Route("Users/{id}")]
        public ActionResult SingleUser(string id)
        {
            try
            {
                var user = userService.GetUser(id);
                if (user == null) return HttpNotFound();
                return View(user);
            }
            catch (Exception e)
            {
                return HttpNotFound();
            }
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

        [System.Web.Mvc.Route("Users/New")]
        public ActionResult NewUser(string id)
        {
            var roles = userService.GetRoles();
            var user = new UserViewModel()
            {
                Email = "email@address.com",
                Name = "Name",
                Roles = roles
            };
            return View(user);
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Route("Users/SaveNewUser")]
        public ActionResult SaveNewUser(UserAndRoleDTO user)
        {
            if (user.Role == null)
                user.Role = RoleNames.DefaultUser;
            userService.Create(user);
            return RedirectToAction("AllUsers", "Users");
        }

        [System.Web.Mvc.Route("Users/ResetPass/{userId}")]
        public ActionResult ChangePassword(string userId)
        {
            var user = userService.GetUser(userId);
            if (user == null) return HttpNotFound();
            userService.ResetPassword(userId);
            return RedirectToAction("AllUsers", "Users");
        }

    }
}