namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC.Controllers;
    using Web.MVC;

    [TestFixture]
    public class TripDetailsRouteTests
    {
        private const int pageId = 558;

        [Test]
        public void TripRouteByIdShouldWorkCorrectlyWithoutSlug()
        {
            string Url = "/Trip/" + pageId;
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TripController>(c => c.Details(pageId));
        }

        [Test]
        public void TripRouteByIdShouldWorkCorrectlyWithSlug()
        {
            string randomSlug = "Sofia-Burgas";
            string Url = "/Trip/" + pageId + "/" + randomSlug;
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TripController>(c => c.Details(pageId));
        }
    }
}
