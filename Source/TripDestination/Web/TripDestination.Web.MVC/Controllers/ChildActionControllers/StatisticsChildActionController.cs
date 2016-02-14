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
        [OutputCache(Duration = CacheTimeConstants.StatisticsTodaysTrips, VaryByParam = "itemsPerRow")]
        public ActionResult TodaysStats(int? itemsPerRow)
        {
            var colMdValue = 6;
            if (itemsPerRow != null)
            {
                switch (itemsPerRow)
                {
                    case 1:
                        colMdValue = 12;
                        break;
                    case 2:
                        colMdValue = 6;
                        break;
                    case 3:
                        colMdValue = 4;
                        break;
                    case 4:
                        colMdValue = 3;
                        break;
                    default:
                        colMdValue = 6;
                        break;
                }
            }

            var viewModel = new TodaysStatisticsViewModel()
            {
                ColMdValue = colMdValue,
                TodayCreatedTrips = this.StatisticsServices.TripsGetTodayCreatedCount(),
                TodayInProgressTrips = this.StatisticsServices.TripsGetTodayInProgressCount(),
                TodayFinishedTrips = this.StatisticsServices.TripsGetTodayFinishedCount(),
                TodayTopDestinationTown = this.StatisticsServices.TripsGetTodayTopDestination()
            };

            return this.PartialView("~/Views/Shared/_TodaysStatisticsPartial.cshtml", viewModel);
        }
    }
}