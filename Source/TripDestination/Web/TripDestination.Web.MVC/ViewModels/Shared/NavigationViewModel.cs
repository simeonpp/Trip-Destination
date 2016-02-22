namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System.Collections.Generic;

    public class NavigationViewModel
    {
        public IEnumerable<PageViewModel> Pages { get; set; }

        public string CurrentUsername { get; set; }

        public string CurrentUserFullName { get; set; }

        public IEnumerable<TripNotificationViewModel> Notifications { get; set; }
    }
}