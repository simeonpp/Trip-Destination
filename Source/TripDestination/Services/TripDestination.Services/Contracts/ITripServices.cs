namespace TripDestination.Services.Contracts
{
    using System;
    using TripDestination.Data.Models;
    using System.Linq;

    public interface ITripServices
    {
        Trip Create(
            int fromTownId,
            int toTownId,
            DateTime dateOfLeaving,
            int availableSeats,
            string placeOfLeaving,
            bool pickUpFromAddress,
            string description,
            DateTime ETA,
            decimal price,
            string driverId);
        
        IQueryable<Trip> GetForDay(DateTime day);
    }
}
