using Ninject.Modules;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;

namespace ToDoList.Web.Helpers
{
    public class ToDoTaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IToDoListService>().To<ToDoListService>();
        }
    }
}