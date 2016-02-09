namespace TripDestination.Web.MVC.Controllers
{
    using Ninject;
    using Services.Contracts;
    using System.Web.Mvc;
    using ViewModels.Statistics;
    public class StatisticsController : BaseController
    {
        [Inject]
        public IStatisticsServices StatisticsServices { get; set; }

        public ActionResult Index()
        {
            StatisticsViewModel viewModel = new StatisticsViewModel()
            {
                TripsCount = this.StatisticsServices.GetTotalTripsCount(),
                TopDestination = this.StatisticsServices.GetTopDestination(),
                UsersCount = this.StatisticsServices.GetUserCount(),
                DriversCount = this.StatisticsServices.GetDriversCount(),
                AverageTripRating = 0 /* this.StatisticsServices.GetAverateTripRating() */,
                TripViews = 0 /* this.StatisticsServices.GetTripViews() */,
                TodayCreatedTrips = this.StatisticsServices.TripsGetTodayCreatedCount(),
                TodayInProgressTrips = this.StatisticsServices.TripsGetTodayInProgressCount(),
                TodayFinishedTrips = this.StatisticsServices.TripsGetTodayFinishedCount(),
                TodayTopDestinationTown = this.StatisticsServices.TripsGetTodayTopDestination()
            };

            return this.View(viewModel);
        }
    }
}