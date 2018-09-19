using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ToDoList.Web.Helpers
{
    public static class Check
    {
        public const string AdminRole = "admin";

        public static bool IsAdmin(IPrincipal user)
        {
            return user.IsInRole(AdminRole);
        }
    }
}