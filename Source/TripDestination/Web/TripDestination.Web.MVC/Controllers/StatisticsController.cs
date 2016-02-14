namespace TripDestination.Web.MVC.Controllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.Statistics;

    public class StatisticsController : BaseController
    {
        public StatisticsController(IStatisticsServices statisticsServices)
        {
            this.StatisticsServices = statisticsServices;
        }

        public IStatisticsServices StatisticsServices { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            StatisticsViewModel viewModel = new StatisticsViewModel()
            {
                TripsCount = this.StatisticsServices.GetTotalTripsCount(),
                TopDestination = this.StatisticsServices.GetTopDestination(),
                UsersCount = 0 /* this.StatisticsServices.GetUserCount() */,
                DriversCount = this.StatisticsServices.GetDriversCount(),
                AverageTripRating = 0 /* this.StatisticsServices.GetAverateTripRating() */,
                TripViews = 0 /* this.StatisticsServices.GetTripViews() */
            };

            return this.View(viewModel);
        }
    }
}