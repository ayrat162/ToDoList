﻿using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Controllers
{
    public class TasksController : Controller
    {
        private ApplicationDbContext _context;
        public TasksController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        private List<ToDoTask> GetTasks()
        {
            return _context.ToDoTasks.Include(t => t.Classification).ToList();
        }

        public ActionResult Index()
        {
            var viewModel = new AllTasksViewModel {Tasks = GetTasks() };
            return View(viewModel);
        }

        [Route("Tasks/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Info(int id)
        {
            var toDoTask = GetTasks().SingleOrDefault(c => c.Id == id);
            if (toDoTask == null) return HttpNotFound();
            return View(toDoTask);
        }

        [Route("Tasks/New")]
        public ActionResult New()
        {
            var classifications = _context.Classifications.ToList();
            var viewModel = new EditTaskViewModel
            {
                Classifications = classifications,
                ToDoTasks = _context.ToDoTasks.ToList()
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
                _context.ToDoTasks.Add(new ToDoTask()
                {
                    Description = toDoTask.Description,
                    ClassificationId = toDoTask.ClassificationId,
                    DueDateTime = toDoTask.DueDateTime,
                    Status = toDoTask.Status,
                    ConnectedtoDoTaskId = toDoTask.ConnectedtoDoTaskId,
                    Image = imageData
                });
            }
            else // upoloading edited item data
            {
                var toDoTaskInDb = _context.ToDoTasks.Single(t => t.Id == toDoTask.Id);
                toDoTaskInDb.Description = toDoTask.Description;
                toDoTaskInDb.DueDateTime = toDoTask.DueDateTime;
                toDoTaskInDb.ClassificationId = toDoTask.ClassificationId;
                toDoTaskInDb.ConnectedtoDoTaskId = toDoTask.ConnectedtoDoTaskId;
                toDoTaskInDb.Status = toDoTask.Status;
                if (imageData != null)
                    toDoTaskInDb.Image = imageData;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Tasks");
        }

        [Route("Tasks/Delete/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Delete(int id)
        {
            _context.ToDoTasks.Remove(_context.ToDoTasks.Single(t => t.Id == id));
            _context.SaveChanges();
            return RedirectToAction("Index", "Tasks");
        }

        [Route("Tasks/Edit/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult Edit(int id)
        {
            var toDoTask = _context.ToDoTasks.SingleOrDefault(t => t.Id == id);
            if (toDoTask == null)
            {
                return HttpNotFound();
            }
            var viewModel = new EditTaskViewModel
            {
                Classifications = _context.Classifications.ToList(),
                ToDoTasks = _context.ToDoTasks.ToList(),
                ToDoTask = toDoTask
            };
            return View(viewModel);
        }
    }
}