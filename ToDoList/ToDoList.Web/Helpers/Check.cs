using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ToDoList.Web.Helpers
{
    public static class Check
    {
        public static bool IsAdmin(IPrincipal user)
        {
            return user.IsInRole("admin");
        }
    }
}