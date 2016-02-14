namespace TripDestination.Web.MVC.ViewModels.ChildAction
{
    using System.Collections.Generic;
    using Shared;

    public class TripsListViewModel
    {
        public IEnumerable<TripListViewModel> Trips { get; set; }
    }
}