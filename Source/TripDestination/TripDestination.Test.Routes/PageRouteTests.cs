namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC.Controllers;
    using Web.MVC;

    [TestFixture]
    public class PageRouteTests
    {
        [Test]
        public void RouteByIdShouldWorkCorrectly()
        {
            const int pageId = 15;

            string Url = "/Page/" + pageId;
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<PageController>(c => c.Index(pageId));
        }
    }
}
