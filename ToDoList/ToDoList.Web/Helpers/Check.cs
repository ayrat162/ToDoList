﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ToDoList.Web.Helpers
{
    public static class Check
    {
<<<<<<< HEAD
        public const string AdminRole = "admin";

        public static bool IsAdmin(IPrincipal user)
        {
            return user.IsInRole(AdminRole);
=======
        public const string Admin = "admin";
        public static bool IsAdmin(IPrincipal user)
        {
            return user.IsInRole(Admin);
>>>>>>> dev
        }
    }
}