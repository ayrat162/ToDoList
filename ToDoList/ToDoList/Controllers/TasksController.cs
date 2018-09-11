using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ApplicationUser CurrentUser()
        {
            return System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>()
                .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
        }

        public ActionResult Index()
        {
            var viewModel = new AllTasksViewModel {Tasks = Repository.GetTasks() };
            return View(viewModel);
        }

        [Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Info(int id)
        {
            var toDoTask = Repository.FindToDoTask(id);
            if (toDoTask == null) return HttpNotFound();
            if (toDoTask.User == null || toDoTask.User.Id != CurrentUser().Id) return RedirectToAction("Index", "Tasks");
            return View(toDoTask);
        }

        [Route("Tasks/New")]
        public ActionResult New()
        {
            var viewModel = new TaskViewModel
            {
                Classifications = Repository.GetClassifications(),
                ToDoTasks = Repository.GetTasks()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Save(ToDoTask toDoTask, HttpPostedFileBase uploadImage)
        {
            byte[] imageData = null;
            if (uploadImage != null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
            }

            if (toDoTask.Id == 0) // uploading new item
            {
                Repository.AddToDoTask(new ToDoTask()
                {
                    Description = toDoTask.Description,
                    ClassificationId = toDoTask.ClassificationId,
                    DueDateTime = toDoTask.DueDateTime,
                    Status = toDoTask.Status,
                    ConnectedtoDoTaskId = toDoTask.ConnectedtoDoTaskId,
                    UserId = CurrentUser().Id,
                    Image = imageData
                });
            }
            else // uploading edited item data
            {
                var toDoTaskInDb = Repository.FindToDoTask(toDoTask.Id);
                if (toDoTaskInDb.User.Id != CurrentUser().Id) return RedirectToAction("Index", "Tasks");
                toDoTaskInDb.Description = toDoTask.Description;
                toDoTaskInDb.DueDateTime = toDoTask.DueDateTime;
                toDoTaskInDb.ClassificationId = toDoTask.ClassificationId;
                toDoTaskInDb.ConnectedtoDoTaskId = toDoTask.ConnectedtoDoTaskId;
                toDoTaskInDb.Status = toDoTask.Status;
                if (imageData != null)
                    toDoTaskInDb.Image = imageData;
                Repository.Save();
            }
            return RedirectToAction("Index", "Tasks");
        }

        [Route("Tasks/Delete/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Delete(int id)
        {
            Repository.RemoveToDoTask(id);
            return RedirectToAction("Index", "Tasks");
        }

        [Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Edit(int id)
        {
            var toDoTask = Repository.FindToDoTask(id);
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            var viewModel = new TaskViewModel
            {
                Classifications = Repository.GetClassifications(),
                ToDoTasks = Repository.GetTasks(),
                ToDoTask = toDoTask
            };
            return View(viewModel);
        }
    }
}