namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Common.Infrastructure.Constants;
    using Services.Data.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;
    using ViewModels.ChildAction;
    using ViewModels.Shared;

    public class HomeChildActionController : Controller
    {
        public HomeChildActionController(ITripServices tripServices)
        {
            this.TripServices = tripServices;
        }

        public ITripServices TripServices { get; set; }

        [ChildActionOnly]
        [OutputCache(Duration = CacheTimeConstants.HomeTodayTrips)]
        public ActionResult TodayTripsPartial()
        {
            var todayTrips = this.TripServices
                    .GetTodayTrips(WebApplicationConstants.HomepageTripsPerSectionCount)
                    .To<TripListViewModel>()
                    .ToList();

            var viewModel = new TripsListViewModel()
            {
                Trips = todayTrips
            };

            return this.PartialView("~/Views/Home/_TodayTripsPartial.cshtml", viewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = CacheTimeConstants.HomeLatestTrips)]
        public ActionResult LatesTripsPartial()
        {
            var latestTrips = this.TripServices
                    .GetLatest(WebApplicationConstants.HomepageTripsPerSectionCount)
                    .To<TripListViewModel>()
                    .ToList();

            var viewModel = new TripsListViewModel()
            {
                Trips = latestTrips
            };

            return this.PartialView("~/Views/Home/_LatestTripsPartial.cshtml", viewModel);
        }
    }
}