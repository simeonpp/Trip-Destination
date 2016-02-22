namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System.Web.Mvc;

    public class NotificationAjaxController : Controller
    {
        private ITripNotificationServices tripNotificationServices;

        public NotificationAjaxController(ITripNotificationServices tripNotificationServices)
        {
            this.tripNotificationServices = tripNotificationServices;
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ApproveNotification(int id)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceReponse = this.tripNotificationServices.ApproveNotification(id, userId);
            return this.Json(serviceReponse);
        }
    }
}