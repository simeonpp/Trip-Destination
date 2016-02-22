namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC.Controllers;
    using Web.MVC;

    [TestFixture]
    public class TripRouteTests
    {
        private const int pageId = 558;

        [Test]
        public void TripRouteDetailsShouldWorkCorrectlyWithoutSlug()
        {
            string Url = string.Format("/Trip/{0}", pageId);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TripController>(c => c.Details(pageId));
        }

        [Test]
        public void TripRouteDetailsShouldWorkCorrectlyWithSlug()
        {
            string randomSlug = "Sofia-Burgas";
            string Url = string.Format("/Trip/{0}/{1}", pageId, randomSlug);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TripController>(c => c.Details(pageId));
        }

        [Test]
        public void TripRouteListShouldWorkCorrectly()
        {
            string calendarDate = "2016-01-01";
            int page = 1;

            string Url = string.Format("/Trip/List?calendarDate={0}&page={1}", calendarDate, page);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<TripController>(c => c.List(calendarDate, page));
        }
    }
}
