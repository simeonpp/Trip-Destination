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
                .Returns(new List<Trip>
                {
                    new Trip() { From = new Town { Name = fromTown }, To = new Town { Name = toTown } }
                }.AsQueryable());
            tripServicesMock.Setup(x => x.GetLatest(It.IsAny<int>()))
                .Returns(new List<Trip>
                {
                    new Trip() { From = new Town { Name = fromTown }, To = new Town { Name = toTown } },
                    new Trip() { From = new Town { Name = toTown }, To = new Town { Name = fromTown } }
                }.AsQueryable());

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

        [Test]
        public void IndexShouldRenderCorrectViewWithCorrectTodayTrips()
        {
            HomeController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomepageViewModel>(
                    vm =>
                    {
                        Assert.AreEqual(1, vm.TodayTrips.Count());
                        Assert.AreEqual(fromTown, vm.TodayTrips.FirstOrDefault().From.Name);
                        Assert.AreEqual(toTown, vm.TodayTrips.FirstOrDefault().To.Name);
                    }
                );
        }

        [Test]
        public void IndexShouldRenderCorrectViewWithCorrectLatestTrips()
        {
            HomeController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomepageViewModel>(
                    vm =>
                    {
                        Assert.AreEqual(2, vm.LatestTrips.Count());
                        Assert.AreEqual(fromTown, vm.LatestTrips.FirstOrDefault().From.Name);
                        Assert.AreEqual(toTown, vm.LatestTrips.FirstOrDefault().To.Name);
                    }
                );
        }
    }
}
