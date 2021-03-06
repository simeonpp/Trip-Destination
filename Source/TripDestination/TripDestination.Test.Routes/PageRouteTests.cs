﻿namespace TripDestination.Test.Routes
{
    using System.Web.Routing;
    using MvcRouteTester;
    using NUnit.Framework;
    using Web.MVC.Controllers;
    using Web.MVC;

    [TestFixture]
    public class PageRouteTests
    {
        private const int pageId = 15;

        [Test]
        public void PageRouteByIdShouldWorkCorrectlyWithoutSlug()
        {
            string Url = string.Format("/Page/{0}", pageId);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<PageController>(c => c.Index(pageId));
        }

        [Test]
        public void PageRouteByIdShouldWorkCorrectlyWithSlug()
        {
            string randomSlug = "adasdadqwdwe";
            string Url = string.Format("/Page/{0}/{1}", pageId, randomSlug);
            var routeCollection = new RouteCollection();
            RouteConfig.RegisterRoutes(routeCollection);
            routeCollection.ShouldMap(Url).To<PageController>(c => c.Index(pageId));
        }
    }
}
