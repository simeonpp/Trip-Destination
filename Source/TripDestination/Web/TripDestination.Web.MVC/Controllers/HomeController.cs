namespace TripDestination.Web.MVC.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Web.MVC.ViewModels.Home;
    using ViewModels.Shared;
    using Services.Data.Contracts;
    using Services.Web.Helpers.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;

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
                    })
                    .ToList(),
                CacheTimeConstants.HomeTopDestination);

            var todayTrips = this.Cache.Get(
                "todayTrips",
                () => this.TripServices
                    .GetTodayTrips(WebApplicationConstants.HomepageTripsPerSectionCount)
                    .To<TripListViewModel>()
                    .ToList(),
                CacheTimeConstants.HomeTodayTrips);

            var latestTrips = this.Cache.Get(
                "latestTrips",
                () => this.TripServices
                    .GetLatest(WebApplicationConstants.HomepageTripsPerSectionCount)
                    .To<TripListViewModel>()
                    .ToList(),
                CacheTimeConstants.HomeLatestTrips);

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