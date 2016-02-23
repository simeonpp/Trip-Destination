namespace TripDestination.Web.MVC.ViewModels.Home
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Views.Trip;

    public class HomepageViewModel : TripSearchInputModel
    {
        public IEnumerable<TopDestinationVIewModel> TopDestinations { get; set; }

        public IEnumerable<SelectListItem> TownsSelectList { get; set; }
    }
}