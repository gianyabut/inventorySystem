using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Inventory.LunarMed.Data.Entities;
using Inventory.LunarMed.Web.Business.Interfaces;
using Inventory.LunarMed.Web.Business.Repository.Base;
using Inventory.LunarMed.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Inventory.LunarMed.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var container = new Container();
            //container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            ////container.Register<IUserStore<ApplicationUser>>(() => new UserStore<ApplicationUser>());
            ////container.Register<IGenericRepository<Client>, GenericRepository<Client>>();

            //// This is an extension method from the integration package.
            //container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            //container.Verify();
            //DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

        }
    }
}
