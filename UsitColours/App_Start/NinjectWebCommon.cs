[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(UsitColours.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(UsitColours.App_Start.NinjectWebCommon), "Stop")]

namespace UsitColours.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Services.Contracts;
    using Services;
    using Services.Contracts.Factories;
    using Ninject.Extensions.Factory;
    using Data.Contracts;
    using Data;
    using AutoMapper;
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
            kernel.Bind<IDbContext>().To<ApplicationDbContext>().InRequestScope();
            kernel.Bind<ILocationFactory>().ToFactory().InRequestScope();
            kernel.Bind<IAirlineFactory>().ToFactory().InRequestScope();
            kernel.Bind<IAirportFactory>().ToFactory().InRequestScope();
            kernel.Bind<IFlightFactory>().ToFactory().InRequestScope();
            kernel.Bind<IJobFactory>().ToFactory().InRequestScope();
            kernel.Bind<ITicketFactory>().ToFactory().InRequestScope();
            kernel.Bind<IMappedClassFactory>().ToFactory().InRequestScope();

            kernel.Bind<IUsitData>().To<UsitData>().InRequestScope();
            kernel.Bind<ICityService>().To<CityService>().InRequestScope();
            kernel.Bind<IMappingService>().To<MappingService>().InRequestScope();
            kernel.Bind<ICountryService>().To<CountryService>().InRequestScope();
            kernel.Bind<IAirlineService>().To<AirlineService>().InRequestScope();
            kernel.Bind<IAirportService>().To<AirportService>().InRequestScope();
            kernel.Bind<IFlightService>().To<FlightService>().InRequestScope();
            kernel.Bind<IJobService>().To<JobService>().InRequestScope();
            kernel.Bind<ITicketService>().To<TicketService>().InRequestScope();
            kernel.Bind<IUserService>().To<UserService>().InRequestScope();
        }
    }
}
