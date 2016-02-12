namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper.QueryableExtensions;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Web.MVC.ViewModels.Home;
    using ViewModels.Shared;
    using Services.Data.Contracts;
    using Services.Web.Helpers.Contracts;
    public class HomeController : BaseController
    {
        public HomeController(ITripServices tripServices, ITripHelper tripHelper)
        {
            this.TripServices = tripServices;
            this.TripHelper = tripHelper;
        }

        public ITripServices TripServices { get; set; }

        public ITripHelper TripHelper { get; set; }

        public ActionResult Index()
        {
            var topDestinations = this.Cache.Get(
                "topDestinations",
                () => this.TripHelper
                    .GetTopDestinations()
                    .Select(t => new TopDestinationVIewModel
                    {
                        FromTown = t.Item1,
                        ToTown = t.Item2
                    }),
                60 * 15);

            var todayTrips = this.TripServices.GetTodayTrips(WebApplicationConstants.HomepageTripsPerSection)
                .ProjectTo<TripListViewModel>(this.MapperConfiguration)
                .ToList();

            var latestTrips = this.TripServices.GetLatest(WebApplicationConstants.HomepageTripsPerSection)
                .ProjectTo<TripListViewModel>(this.MapperConfiguration)
                .ToList();

            HomepageViewModel viewModel = new HomepageViewModel()
            {
                TopDestinations = topDestinations,
                TodayTrips = todayTrips,
                LatestTrips = latestTrips
            };

            return this.View(viewModel);
        }
    }
}