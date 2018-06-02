[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Inventory.LunarMed.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Inventory.LunarMed.Web.App_Start.NinjectWebCommon), "Stop")]

namespace Inventory.LunarMed.Web.App_Start
{
    using System;
    using System.Web;
    using Inventory.LunarMed.Data.Entities;
    using Inventory.LunarMed.Web.Business.Interfaces;
    using Inventory.LunarMed.Web.Business.Repository.Base;
    using Inventory.LunarMed.Web.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().WithConstructorArgument("context", kernel.Get<ApplicationDbContext>());
            kernel.Bind<UserManager<ApplicationUser>>().ToSelf();

            kernel.Bind<HttpContextBase>().ToMethod(ctx => new HttpContextWrapper(HttpContext.Current)).InTransientScope();

            kernel.Bind<ApplicationSignInManager>().ToMethod((context) =>
            {
                var cbase = new HttpContextWrapper(HttpContext.Current);
                return cbase.GetOwinContext().Get<ApplicationSignInManager>();
            });

            kernel.Bind<ApplicationUserManager>().ToSelf();

            // Entities
            kernel.Bind(typeof(IGenericRepository<Client>)).To(typeof(GenericRepository<Client>));
            kernel.Bind(typeof(IGenericRepository<Collection>)).To(typeof(GenericRepository<Collection>));
            kernel.Bind(typeof(IGenericRepository<Order>)).To(typeof(GenericRepository<Order>));
            kernel.Bind(typeof(IGenericRepository<OrderDetails>)).To(typeof(GenericRepository<OrderDetails>));
            kernel.Bind(typeof(IGenericRepository<Stock>)).To(typeof(GenericRepository<Stock>));
            kernel.Bind(typeof(IGenericRepository<UnitSize>)).To(typeof(GenericRepository<UnitSize>));
            kernel.Bind(typeof(IGenericRepository<Generic>)).To(typeof(GenericRepository<Generic>));
            kernel.Bind(typeof(IGenericRepository<Brand>)).To(typeof(GenericRepository<Brand>));
        }        
    }
}
