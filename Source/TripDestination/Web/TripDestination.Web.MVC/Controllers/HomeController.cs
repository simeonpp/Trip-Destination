namespace TripDestination.Web.MVC.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Web.MVC.ViewModels.Home;
    using Services.Data.Contracts;
    using Services.Web.Helpers.Contracts;
    using Services.Web.Providers.Contracts;
    public class HomeController : BaseController
    {
        private readonly ITownProvider townProvider;

        public HomeController(ITripServices tripServices, ITripHelper tripHelper, ITownProvider townProvider)
        {
            this.TripServices = tripServices;
            this.TripHelper = tripHelper;
            this.townProvider = townProvider;
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
                TopDestinations = topDestinations,
                TownsSelectList = this.townProvider.GetTowns()
            };

            return this.View(viewModel);
        }
    }
}