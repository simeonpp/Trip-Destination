namespace TripDestination.Web.MVC.ViewModels.Home
{
    using System.Collections.Generic;
    using Shared;
    using System;

    public class HomepageViewModel
    {
        public IEnumerable<TopDestinationVIewModel> TopDestinations { get; set; }

        public IEnumerable<TripListViewModel> TodayTrips { get; set; }

        public IEnumerable<TripListViewModel> LatestTrips { get; set; }
    }
}