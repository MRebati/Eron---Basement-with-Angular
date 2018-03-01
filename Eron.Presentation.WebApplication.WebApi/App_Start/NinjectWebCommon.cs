[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Eron.Presentation.WebApplication.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Eron.Presentation.WebApplication.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace Eron.Presentation.WebApplication.WebApi.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Eron.SharedKernel.DependancyResolver.BusinessModules;
    using System.Web.Http;
    using Eron.Presentation.WebApplication.WebApi.Infrastructure;
    using Eron.SharedKernel.DependancyResolver.DataAccessModules;

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

                RegisterRepositories(kernel);
                RegisterUnitsOfWork(kernel);
                RegisterServices(kernel);

                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Services Register Here
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Load(new ApplicationModule());
        }

        /// <summary>
        /// UnitsOfWork Register Here
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterUnitsOfWork(IKernel kernel)
        {
            kernel.Load(new UnitOfWorkModule());
        }

        /// <summary>
        /// Repositories Register Here
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterRepositories(IKernel kernel)
        {
            kernel.Load(new RepositoriesModule());
        }
    }
}
