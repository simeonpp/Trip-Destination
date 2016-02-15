namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class TripInputVIewModel : BaseTripExtentendedModel
    {
        public IEnumerable<SelectListItem> TownsSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList { get; set; }

        public IEnumerable<SelectListItem> AddressPickUpSelectList { get; set; }
    }
}