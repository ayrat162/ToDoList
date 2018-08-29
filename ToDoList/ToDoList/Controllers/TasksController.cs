using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
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
            return View(toDoTask);
        }

        [Route("Tasks/New")]
        public ActionResult New()
        {
            var viewModel = new EditTaskViewModel
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
                    Image = imageData
                });
            }
            else // uploading edited item data
            {
                var toDoTaskInDb = Repository.FindToDoTask(toDoTask.Id);
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
            var viewModel = new EditTaskViewModel
            {
                Classifications = Repository.GetClassifications(),
                ToDoTasks = Repository.GetTasks(),
                ToDoTask = toDoTask
            };
            return View(viewModel);
        }
    }
}