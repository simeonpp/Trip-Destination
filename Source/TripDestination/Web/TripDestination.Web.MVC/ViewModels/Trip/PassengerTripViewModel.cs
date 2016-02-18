namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Shared;

    public class PassengerTripViewModel : IMapFrom<PassengerTrip>
    {
        public BaseUserViewModel User { get; set; }

        public bool Approved { get; set; }
    }
}