namespace TripDestination.Web.MVC.Controllers
{
    using Common.Infrastructure.Constants;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.Statistics;

    public class StatisticsController : BaseController
    {
        private readonly IStatisticsServices statisticsServices;

        private readonly IUserServices userServices;

        public StatisticsController(IStatisticsServices statisticsServices, IUserServices userServices)
        {
            this.statisticsServices = statisticsServices;
            this.userServices = userServices;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tripCount = this.Cache.Get(
                "statisticsTripCpimt",
                () => this.statisticsServices.GetTotalTripsCount(),
                CacheTimeConstants.StatisticsTodaysTrips);

            var topDestomatopm = this.Cache.Get(
                "topDestination",
                () => this.statisticsServices.GetTopDestination(),
                CacheTimeConstants.StatisticsTodaysTrips);

            var usersCount = this.Cache.Get(
                "usersCount",
                () => this.userServices.GetUsersCountInRole(RoleConstants.PassengerRole),
                CacheTimeConstants.StatisticsTodaysTrips);

            var driversCount = this.Cache.Get(
                "driversCount",
                () => this.userServices.GetUsersCountInRole(RoleConstants.PassengerRole),
                CacheTimeConstants.StatisticsTodaysTrips);

            var averageTripRating = this.Cache.Get(
                "averageTripRating",
                () => string.Format("{0:N2}", this.statisticsServices.GetAverateTripRating()),
                CacheTimeConstants.StatisticsTodaysTrips);

            var tripViews = this.Cache.Get(
                "tripViews",
                () => this.statisticsServices.GetTripViews(),
                CacheTimeConstants.StatisticsTodaysTrips);

            StatisticsViewModel viewModel = new StatisticsViewModel()
            {
                TripsCount = tripCount,
                TopDestination = topDestomatopm,
                UsersCount = usersCount,
                DriversCount = driversCount,
                AverageTripRating = averageTripRating,
                TripViews = tripViews
            };

            return this.View(viewModel);
        }
    }
}