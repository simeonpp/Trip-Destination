namespace TripDestination.Services.Data.Contracts
{
    using System;
    using TripDestination.Data.Models;
    using System.Linq;
    using System.Collections.Generic;
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

        IQueryable<PassengerTrip> GetPassengers(Trip trip);

        Trip Edit(
            int tripId,
            DateTime dateOfLeaving,
            int leftAvailableSeats,
            string placeOfLeaving,
            bool pickUpFromAddress,
            string description,
            DateTime ETA,
            IEnumerable<string> usernamesToBeRemoved);

        int GetAvailableLeftSeatsCount(int tripId);


    }
}
