namespace TripDestination.Web.MVC.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Web.MVC.ViewModels.Home;
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

        [HttpGet]
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

            HomepageViewModel viewModel = new HomepageViewModel()
            {
                TopDestinations = topDestinations
            };

            return this.View(viewModel);
        }
    }
}