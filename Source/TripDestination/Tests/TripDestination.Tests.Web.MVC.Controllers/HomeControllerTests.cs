namespace TripDestination.Tests.Web.MVC.Controllers
{
    using Common.Infrastructure.Mapping;
    using NUnit.Framework;
    using TripDestination.Web.MVC.Controllers;
    using Moq;
    using Services.Data.Contracts;
    using Data.Models;
    using System.Linq;
    using System.Collections.Generic;
    using Services.Web.Helpers.Contracts;
    using System;
    using TestStack.FluentMVCTesting;
    using TripDestination.Web.MVC.ViewModels.Home;
    using Services.Web.Services;

    [TestFixture]
    public class HomeControllerTests
    {
        private const string fromTown = "Sofia";
        private const string toTown = "Burgas";
        
        HomeController HomeController;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Register(typeof(HomeController).Assembly);

            var tripServicesMock = new Mock<ITripServices>();
            tripServicesMock.Setup(x => x.GetTodayTrips(It.IsAny<int>()))
                .Returns(new List<Trip>().AsQueryable());
            tripServicesMock.Setup(x => x.GetLatest(It.IsAny<int>()))
                .Returns(new List<Trip>().AsQueryable());

            var tripHelperMock = new Mock<ITripHelper>();
            tripHelperMock.Setup(x => x.GetTopDestinations())
                .Returns(new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>(fromTown, toTown)
                });

            var homeController = new HomeController(tripServicesMock.Object, tripHelperMock.Object);
            homeController.Cache = new HttpCacheServices();

            HomeController = homeController;
        }

        [Test]
        public void IndexShouldRenderCorrectViewWithCorrectTopDestinations()
        {
            HomeController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomepageViewModel>(
                    vm =>
                    {
                        Assert.AreEqual(fromTown, vm.TopDestinations.FirstOrDefault().FromTown);
                        Assert.AreEqual(toTown, vm.TopDestinations.FirstOrDefault().ToTown);
                    }
                );
        }
    }
}
