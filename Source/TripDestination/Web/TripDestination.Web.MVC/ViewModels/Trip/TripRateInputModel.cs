namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using System.Collections.Generic;
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class TripRateInputModel
    {
        public int TripId { get; set; }

        public bool CurrentUserIsDriver { get; set; }

        public ICollection<BaseUserViewModel> Passengers { get; set; }

        public int DriverRating { get; set; }

        public ICollection<int> PassengerRatings { get; set; }
    }
}