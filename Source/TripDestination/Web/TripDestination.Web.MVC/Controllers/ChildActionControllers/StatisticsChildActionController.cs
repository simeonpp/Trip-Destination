namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using ViewModels.Shared;
    using Common.Infrastructure.Constants;
    public class StatisticsChildActionController : Controller
    {
        public StatisticsChildActionController(IStatisticsServices statisticsServices)
        {
            this.StatisticsServices = statisticsServices;
        }

        public IStatisticsServices StatisticsServices { get; set; }

        [ChildActionOnly]
        [OutputCache(Duration = CacheTimeConstants.StatisticsTodaysTrips)]
        public ActionResult TodaysStats()
        {
            var viewModel = new TodaysStatisticsViewModel()
            {
                TodayCreatedTrips = this.StatisticsServices.TripsGetTodayCreatedCount(),
                TodayInProgressTrips = this.StatisticsServices.TripsGetTodayInProgressCount(),
                TodayFinishedTrips = this.StatisticsServices.TripsGetTodayFinishedCount(),
                TodayTopDestinationTown = this.StatisticsServices.TripsGetTodayTopDestination()
            };

            return this.PartialView("~/Views/Shared/_TodaysStatisticsPartial.cshtml", viewModel);
        }
    }
}