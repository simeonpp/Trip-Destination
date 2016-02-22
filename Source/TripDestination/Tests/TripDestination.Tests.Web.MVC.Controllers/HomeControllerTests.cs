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
    using Services.Web.Providers.Contracts;
    using System.Web.Mvc;

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

            var townHelperMock = new Mock<ITownProvider>();
            townHelperMock.Setup(x => x.GetTowns())
                .Returns(new List<SelectListItem>()
                {
                    new SelectListItem { Text = "A", Value = "A" },
                    new SelectListItem { Text = "B", Value = "B" },
                    new SelectListItem { Text = "C", Value = "C" },
                });

            var homeController = new HomeController(tripServicesMock.Object, tripHelperMock.Object, townHelperMock.Object);
            homeController.Cache = new HttpCacheServices();

            HomeController = homeController;
        }

        [Test]
        public void IndexShouldRenderCorrectView()
        {
            HomeController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomepageViewModel>();
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
        public void IndexShouldRenderCorrectViewWithCorrectLatestTripTownsSelectList()
        {
            HomeController.WithCallTo(x => x.Index())
                .ShouldRenderView("Index")
                .WithModel<HomepageViewModel>(
                    vm =>
                    {
                        Assert.AreEqual("A", vm.TownsSelectList.FirstOrDefault().Text);
                        Assert.AreEqual("B", vm.TownsSelectList.ElementAt(1).Text);
                        Assert.AreEqual("C", vm.TownsSelectList.ElementAt(2).Text);
                    }
                );
        }
    }
}
