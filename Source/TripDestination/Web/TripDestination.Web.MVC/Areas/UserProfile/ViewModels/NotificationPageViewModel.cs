namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using MVC.ViewModels.Shared;
    using System.Collections.Generic;

    public class NotificationPageViewModel
    {
        public IEnumerable<NotificationTripViewModel> Notifications { get; set; }

        public BaseUserViewModel User { get; set; }
    }
}