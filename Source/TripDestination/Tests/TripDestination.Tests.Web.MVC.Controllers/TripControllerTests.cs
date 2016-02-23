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
    using System;
    using TestStack.FluentMVCTesting;
    using Services.Web.Providers.Contracts;
    using System.Web.Mvc;
    using TripDestination.Web.MVC.ViewModels.Trip;
    using TripDestination.Web.Infrastructure.Models;
    [TestFixture]
    public class TripControllerTests
    {
        private DateTime day = new DateTime(1900, 1, 1);

        private TripController TripController;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Register(typeof(HomeController).Assembly);
            
            var tripProviderMovk = new Mock<ITripProvider>();
            tripProviderMovk.Setup(x => x.GetTripsPerPageSelectList())
                .Returns(new List<SelectListItem>()
                {
                    new SelectListItem { Text = 1.ToString(), Value = 1.ToString() }
                });

            var ratingServices = new Mock<IRatingServices>();

            var townProviderHelper = new Mock<ITownProvider>();
            townProviderHelper.Setup(x => x.GetTowns())
                .Returns(new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Sofia", Value = "Sofia" }
                });

            var tripProvider = new Mock<ITripProvider>();
            tripProvider.Setup(x => x.GetOrderBySelectList(It.IsAny<string>()))
                .Returns(new List<SelectListItem>()
                {
                    new SelectListItem { Text = "Order by town", Value = "Town" }
                });
            tripProvider.Setup(x => x.GetAvailableSeatsSelectList())
                .Returns(new List<SelectListItem>()
                {
                    new SelectListItem { Text = 1.ToString(), Value = 1.ToString() }
                });

            var dateProviderMock = new Mock<IDateProvider>();
            dateProviderMock.Setup(x => x.CovertDateFromStringToDateTime(It.IsAny <string>()))
                .Returns(this.day);
            dateProviderMock.Setup(x => x.GetWeekAhedDays(this.day))
                .Returns(new List<WeekDay>()
                {
                    new WeekDay { Datetime = this.day, IsActive = true }
                }.AsQueryable());

            var tripServicesMock = new Mock<ITripServices>();
            tripServicesMock.Setup(x => x.GetForDay(It.IsAny<DateTime>()))
                .Returns(new List<Trip>
                {
                    new Trip() { From = new Town { Name = "Sofia" }, To = new Town { Name = "Burgas" } }
                }.AsQueryable());
            tripServicesMock.Setup(x => x.GetByIdWithStatusCheck(It.IsAny<int>()))
                .Returns(new Trip
                {
                    AvailableSeats = 3,
                    FromId = 1,
                    ToId = 2
                });

            var statisticsServiceMock = new Mock<IStatisticsServices>();
            var viewServicesMock = new Mock<IViewServices>();
            var notificationServicesMock = new Mock<INotificationServices>();
            
            var tripController = new TripController(
                tripServicesMock.Object,
                ratingServices.Object,
                townProviderHelper.Object,
                statisticsServiceMock.Object,
                viewServicesMock.Object,
                dateProviderMock.Object,
                tripProvider.Object,
                notificationServicesMock.Object);
            
            this.TripController = tripController;
        }

        [Test]
        public void ListShouldRenderCorrectView()
        {
            this.TripController.WithCallTo(x => x.List("2016-1-1", 1))
                .ShouldRenderView("List")
                .WithModel<TripLstViewModel>();
        }

        [Test]
        public void ListShouldRenderCorrectViewWithCorrectData()
        {
            this.TripController.WithCallTo(x => x.List("2016-1-1", 1))
                .ShouldRenderView("List")
                .WithModel<TripLstViewModel>(
                    vm =>
                    {
                        Assert.IsTrue(vm.WeekDays.FirstOrDefault().IsActive);
                        Assert.AreEqual(vm.WeekDays.FirstOrDefault().Datetime, this.day);
                        Assert.AreEqual(vm.Trips.Count(), 1);
                        Assert.AreEqual(vm.Trips.FirstOrDefault().From.Name, "Sofia");
                        Assert.AreEqual(vm.Trips.FirstOrDefault().To.Name, "Burgas");
                        Assert.AreEqual(vm.TotalPages, 1);
                        Assert.AreEqual(vm.CurrentPage, 1);
                        Assert.AreEqual(vm.TotalFoundTrips, 1);
                    }
                );
        }

    }
}
