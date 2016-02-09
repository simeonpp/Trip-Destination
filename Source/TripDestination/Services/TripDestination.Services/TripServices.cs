namespace TripDestination.Services
{
    using System;
    using Contracts;
    using Data.Models;
    using Data.Data.Repositories;

    public class TripServices : ITripServices
    {
        private IRepository<Trip> tripRepos;

        public TripServices(IRepository<Trip> tripRepos)
        {
            this.tripRepos = tripRepos;
        }

        public Trip Create(int fromTownId, int toTownId, DateTime dateOfLeaving, int availableSeats, string placeOfLeaving, bool pickUpFromAddress, string description, DateTime ETA, decimal price, string driverId)
        {
            Trip trip = new Trip()
            {
                DateOfLeaving = dateOfLeaving,
                FromId = fromTownId,
                ToId = toTownId,
                DriverId = driverId,
                Description = description,
                ETA = ETA,
                PlaceOfLeaving = placeOfLeaving,
                PickUpFromAddress = pickUpFromAddress,
                AvailableSeats = availableSeats,
                Price = price
            };

            this.tripRepos.Add(trip);
            this.tripRepos.SaveChanges();

            return trip;
        }
    }
}
