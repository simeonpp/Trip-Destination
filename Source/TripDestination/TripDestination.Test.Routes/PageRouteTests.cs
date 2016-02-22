namespace TripDestination.Test.Routes
{
    using MvcRouteTester;
    using NUnit.Framework;
    using System.Web.Routing;
    using Web.MVC;
    using Web.MVC.Controllers;

    [TestFixture]
    class PageRouteTests
    {
        [Test]
        public void TestRouteById()
        {
            const int pageId = 15;

            string Url = "/Page/" + pageId;
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<PageController>(c => c.Index(pageId));
        }
    }
}
