using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class PagesController : Controller
    {
        public ActionResult About()
        {
            return View();
        }
    }
}