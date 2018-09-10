using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;

[assembly: OwinStartup(typeof(ToDoList.Web.App_Start.Startup))]

namespace ToDoList.Web.App_Start
{
    public class Startup
    {
        CreatorService creatorService = new CreatorService();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return creatorService.CreateUserService();
        }
    }
}