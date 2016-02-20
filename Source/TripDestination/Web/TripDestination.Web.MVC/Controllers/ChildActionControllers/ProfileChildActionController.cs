namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using Common.Infrastructure.Constants;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.ChildAction;
    public class ProfileChildActionController : Controller
    {
        private IStatisticsServices statisticsServices;

        public ProfileChildActionController(IStatisticsServices statisticsServices)
        {
            this.statisticsServices = statisticsServices;
        }

        //[OutputCache(Duration = CacheTimeConstants.UserProfileStatistics, VaryByParam = "username")]
        public ActionResult UserStatistics(string userId)
        {
            var viewModel = new UserStatisticsViewModel();
            viewModel.CommentsCount = this.statisticsServices.GetUserCommentsCount(userId);
            viewModel.TripsAsDriverCount = this.statisticsServices.GetUserTripsAsDriverCount(userId);
            viewModel.TripsAsPassengerCount = this.statisticsServices.GetUserTripsAsPassengerCount(userId);
            viewModel.TotalTrips = viewModel.TripsAsDriverCount + viewModel.TripsAsPassengerCount;

            return this.PartialView("~/Views/Profile/_UserStatisticsSectionPartial.cshtml", viewModel);
        }
    }
}