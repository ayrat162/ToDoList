using Microsoft.Owin;
using Owin;
using ToDoList.Models;

[assembly: OwinStartupAttribute(typeof(ToDoList.Startup))]
namespace ToDoList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Repository.Connect();
            ConfigureAuth(app);
        }
    }
}
