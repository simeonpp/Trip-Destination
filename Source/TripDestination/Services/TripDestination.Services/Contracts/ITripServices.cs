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

        Trip GetById(int id);

        IQueryable<Trip> GetForDay(DateTime day);

        IQueryable<Trip> GetLatest(int count);

        IQueryable<Trip> GetTodayTrips(int count);

        IQueryable<string> GetTopTownsDestination(bool townsTo = true, int count = 2);
    }
}
