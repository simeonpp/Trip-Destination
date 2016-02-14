namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System.Collections.Generic;

    public class NavigationViewModel
    {
        public IEnumerable<PageViewModel> Pages { get; set; }
    }
}