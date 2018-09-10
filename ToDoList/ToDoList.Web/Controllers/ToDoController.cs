using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    public class ToDoController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
    }
}