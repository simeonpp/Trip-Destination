namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Shared;    
    using System.Collections.Generic;

    public class TripLstViewModel
    {
        public DateTime Date { get; set; }

        public IEnumerable<TripListViewModel> Trips { get; set; }
    }
}