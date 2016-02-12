namespace TripDestination.Web.MVC
{
    using System.Data.Entity;
    using System.Reflection;
    using System.Web.Mvc;

    using Autofac;
    using Autofac.Integration.Mvc;

    using Controllers;

    using Data.Common;

    using Data.Data;
    using TripDestination.Services.Data.Contracts;
    using Services.Web.Services;
    using Services.Web.Services.Contracts;
    using Services.Web.Providers;
    using Services.Web.Providers.Contracts;
    using Services.Web.Helpers.Contracts;
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Register services
            RegisterServices(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DbRepository<>))
                .As(typeof(IDbRepository<>))
                .InstancePerRequest();

            builder.Register(x => new TripDestinationDbContext())
                .As<DbContext>()
                .InstancePerRequest();

            builder.Register(x => new HttpCacheServices())
                .As<ICacheServices>()
                .InstancePerRequest();

            builder.Register(x => new IdentifierProvider())
               .As<IIdentifierProvider>()
                .InstancePerRequest();

            var servicesAssembly = Assembly.GetAssembly(typeof(ITripServices));
            builder.RegisterAssemblyTypes(servicesAssembly).AsImplementedInterfaces();

            var helperssAssembly = Assembly.GetAssembly(typeof(ITripHelper));
            builder.RegisterAssemblyTypes(helperssAssembly).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AssignableTo<BaseController>().PropertiesAutowired();
        }
    }
}