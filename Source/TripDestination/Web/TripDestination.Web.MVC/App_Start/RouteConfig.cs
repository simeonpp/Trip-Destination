using System.Web.Mvc;
using System.Web.Routing;
using TripDestination.Common.Infrastructure.Constants;

namespace TripDestination.Web.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Page",
                url: "Page/{id}/{slug}",
                defaults: new { controller = "Page", action = "Index", slug = UrlParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "TripDetails",
                url: "Trip/{id}/{slug}",
                defaults: new { controller = "Trip", action = "Details", slug = UrlParameter.Optional },
                constraints: new { id = @"\d+" }
            );

            routes.MapRoute(
                name: "MediaImage",
                url: WebApplicationConstants.ImageRouteUrl + "/{username}/{guid}/{size}",
                defaults: new { controller = "Image", action = "Index", size = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
