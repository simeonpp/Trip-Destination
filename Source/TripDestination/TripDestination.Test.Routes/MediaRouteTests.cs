namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC;
    using Web.MVC.Areas.UserProfile.Controllers;
    using Web.MVC.Controllers.Media;
    [TestFixture]
    public class MediaRouteTests
    {
        private const string username = "pesho";
        private const string guid = "aasdas-232-asd";
        private const int size = 480;

        [Test]
        public void MediaRouteDetailsShouldWorkCorrectly()
        {
            string Url = string.Format("/Media/Image/{0}/{1}/{2}", username, guid, size);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<ImageController>(c => c.Index(username, guid, size));
        }
    }
}
