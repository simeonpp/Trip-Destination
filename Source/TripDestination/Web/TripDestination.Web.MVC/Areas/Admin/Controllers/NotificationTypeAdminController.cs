namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MVC.Controllers;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class NotificationTypeAdminController : BaseController
    {
        private readonly INotificationTypeServices notificationTypeServices;

        public NotificationTypeAdminController(INotificationTypeServices notificationTypeServices)
        {
            this.notificationTypeServices = notificationTypeServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult NotificationType_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.notificationTypeServices
                .GetAll()
                .To<NotificationTypeAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}