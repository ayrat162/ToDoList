using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using ToDoList.Core.Infrastructure;
using System.Web.Http;
using System.Web.Routing;
using ToDoList.Web.Helpers;

namespace ToDoList.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            NinjectModule toDoTaskModule = new ToDoTaskModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(toDoTaskModule, serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            kernel.Unbind<ModelValidatorProvider>();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
