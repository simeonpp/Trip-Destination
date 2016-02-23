namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC;
    using Web.MVC.Controllers.Media;
    using Web.MVC.Controllers;
    [TestFixture]
    public class CookiePolicyTests
    {
        private const string username = "pesho";
        private const string guid = "aasdas-232-asd";
        private const int size = 480;

        [Test]
        public void CookiePOlicyShouldWorkCorrectly()
        {
            string Url = "/CookiePolicy";
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<CookiePolicyController>(c => c.Index());
        }
    }
}
