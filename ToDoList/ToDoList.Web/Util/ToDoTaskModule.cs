using Ninject.Modules;
using ToDoList.Core.Interfaces;
using ToDoList.Core.Services;

namespace ToDoList.Web.Util
{
    public class ToDoTaskModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IToDoTaskService>().To<ToDoTaskService>();
        }
    }
}