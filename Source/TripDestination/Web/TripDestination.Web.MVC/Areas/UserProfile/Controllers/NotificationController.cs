namespace TripDestination.Web.MVC.Areas.UserProfile.Controllers
{
    using Common.Infrastructure.Mapping;
    using Microsoft.AspNet.Identity;
    using MVC.Controllers;
    using MVC.ViewModels.Shared;
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Mvc;
    using ViewModels;
    using System.Linq;
    using Services.Web.Providers.Contracts;
    [Authorize]
    public class NotificationController : BaseController
    {
        private readonly ITripNotificationServices tripNotificationServices;

        private readonly INotificationProvider notificationProvider;

        private readonly IUserServices userSerives;

        public NotificationController(ITripNotificationServices tripNotificationServices, IUserServices userSerives, INotificationProvider notificationProvider)
        {
            this.tripNotificationServices = tripNotificationServices;
            this.userSerives = userSerives;
            this.notificationProvider = notificationProvider;
        }

        [HttpGet]
        public ActionResult Index()
        {

            string userId = this.User.Identity.GetUserId();
            var viewModel = new NotificationPageViewModel();

            var notification = this.tripNotificationServices.GetForUser(userId);
            viewModel.Notifications = notification
                .To<NotificationTripViewModel>()
                .ToList();

            foreach (var notif in viewModel.Notifications)
            {
                if (notif.Seen == false)
                {
                    this.tripNotificationServices.SetAsSeen(notif.Id);
                }

                notif.ActionModel = this.notificationProvider.GetAvailableActionModel(notif.Type.Key, notif.ActionHasBeenTaken, notif.Trip.Id, userId);
            }

            var user = this.userSerives.GetById(userId);
            viewModel.User = this.Mapper.Map<BaseUserViewModel>(user);

            return this.View("~/Areas/UserProfile/Views/Notification/Index.cshtml", viewModel);
        }

    }
}