using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Web.ViewModels;

namespace ToDoList.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(db.ToDoTasks.ToList());
        }

        public ActionResult ViewTask(int? id)
        {
            if (id == null)
                return HttpNotFound();
            var toDoTask = db.ToDoTasks.Find(id);
            if (toDoTask == null)
                return HttpNotFound();
            var toDoTaskViewModel = new ToDoTaskViewModel { Description = toDoTask.Description, DueDateTime = toDoTask.DueDateTime };
            return View(toDoTaskViewModel);
        }







        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}