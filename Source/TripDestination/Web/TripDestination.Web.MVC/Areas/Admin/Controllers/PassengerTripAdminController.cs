namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class PassengerTripAdminController : Controller
    {
        private readonly IPassengerTripServices passengerTripServices;

        public PassengerTripAdminController(IPassengerTripServices passengerTripServices)
        {
            this.passengerTripServices = passengerTripServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult PassengerTrip_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.passengerTripServices
                .GetAll()
                .To<PassengerTripAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PassengerTrip_Destroy([DataSourceRequest]DataSourceRequest request, PassengerTripAdminViewModel passengerTrip)
        {
            this.passengerTripServices.Delete(passengerTrip.Id);
            return this.Json(new[] { passengerTrip }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}