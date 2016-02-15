namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Web.MVC.ViewModels.Shared;
    using TripDestination.Data.Models;
    using System;
    public class TripDetailedViewModel : IMapFrom<Trip>
    {
        public Town From { get; set; }

        public Town To { get; set; }

        public DateTime DateOfLeaving { get; set; }

        public string PlaceOfLeaving { get; set; }

        public int AvailableSeats { get; set; }

        public TripStatus Status { get; set; }

        public bool PickUpFromAddress { get; set; }

        public decimal Price { get; set; }

        public User Driver { get; set; }
    }
}