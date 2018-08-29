using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Controllers
{
    public class PagesController : Controller
    {
        // GET: About
        public ActionResult AboutMe()
        {
            return View();
        }
    }
}