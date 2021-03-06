﻿namespace TripDestination.Web.MVC
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Common.Infrastructure.Mapping;
    using System.Reflection;

    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Register();
            ViewEnginesConfig.Register(ViewEngines.Engines);
            AutofacConfig.Register();
            AutoMapperConfig.Register(Assembly.GetExecutingAssembly());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
