using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult Index()
        {
            var viewModel = new TasksViewModel {Tasks = GetTasks() };
            return View(viewModel);
        }

        [Route("ShowTask/{id:regex(\\d):range(0, 1000000)}")]
        public ActionResult ShowTask(int id)
        {
            var toDoTask = GetTasks().SingleOrDefault(c => c.Id == id);
            if (toDoTask == null) return HttpNotFound();

            var viewModel = new SingleTaskViewModel()
            {
                ToDoTask = toDoTask
            };
            return View(viewModel);
        }

        private List<ToDoTask> GetTasks()
        {
            return _context.ToDoTasks.Include(c => c.Classification).ToList();
        }
    }
}