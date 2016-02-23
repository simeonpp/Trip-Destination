namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class TripNotificationAdminController : Controller
    {
        private readonly ITripNotificationServices tripNotificationServices;

        public TripNotificationAdminController(ITripNotificationServices tripNotificationServices)
        {
            this.tripNotificationServices = tripNotificationServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult TripNotification_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.tripNotificationServices
                .GetAll()
                .To<TripNotificationAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}