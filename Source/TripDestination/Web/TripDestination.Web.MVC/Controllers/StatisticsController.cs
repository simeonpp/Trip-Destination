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
            StatisticsViewModel viewModel = new StatisticsViewModel()
            {
                TripsCount = this.statisticsServices.GetTotalTripsCount(),
                TopDestination = this.statisticsServices.GetTopDestination(),
                UsersCount = this.userServices.GetUsersCountInRole(RoleConstants.PassengerRole),
                DriversCount = this.userServices.GetUsersCountInRole(RoleConstants.PassengerRole),
                AverageTripRating = string.Format("{0:N2}", this.statisticsServices.GetAverateTripRating()),
                TripViews = this.statisticsServices.GetTripViews()
            };

            return this.View(viewModel);
        }
    }
}