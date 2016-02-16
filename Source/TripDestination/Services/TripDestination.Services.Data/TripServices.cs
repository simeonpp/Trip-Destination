namespace TripDestination.Data.Services
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using TripDestination.Services.Data.Contracts;
    using Common;
    using Models;

    public class TripServices : ITripServices
    {
        private IDbRepository<Trip> tripRepos;

        private IDbRepository<PassengerTrip> passengerTripsRepos;

        public TripServices(IDbRepository<Trip> tripRepos, IDbRepository<PassengerTrip> passengerTripsRepos)
        {
            this.tripRepos = tripRepos;
            this.passengerTripsRepos = passengerTripsRepos;
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
            this.tripRepos.Save();

            return trip;
        }

        public Trip GetById(int id)
        {
            Trip trip = this.tripRepos
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            return trip;
        }

        public IQueryable<Trip> GetForDay(DateTime day)
        {
            var trips = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == day
                            && t.Status == TripStatus.Open
                            && t.AvailableSeats >= 1)
                .OrderByDescending(t => t.DateOfLeaving);

            return trips;
        }

        public IQueryable<Trip> GetLatest(int count)
        {
            var trips = this.tripRepos
                .All()
                .OrderByDescending(t => t.CreatedOn)
                .Take(count);

            return trips;
        }

        public IQueryable<Trip> GetTodayTrips(int count)
        {
            var trips = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == DateTime.Today)
                .OrderByDescending(t => t.CreatedOn)
                .Take(count);

            return trips;
        }

        public IQueryable<string> GetTopTownsDestination(bool townsFrom = true, int count = 2)
        {
            var towns = this.tripRepos
               .All()
               .GroupBy(t => townsFrom == true ? t.From.Name : t.To.Name)
               .Select(group => new { TownName = group.Key, Count = group.Count() })
               .OrderByDescending(e => e.Count)
               .Take(count)
               .Select(t => t.TownName);

            return towns;
        }

        public IQueryable<PassengerTrip> GetPassengers(Trip trip)
        {
            var passengers = this.passengerTripsRepos.All()
                .Where(pt => pt.Trip == trip && pt.Approved == true)
                .OrderBy(pt => pt.CreatedOn);

            return passengers;
        }

        public Trip Edit(int tripId, DateTime dateOfLeaving, int leftAvailableSeats, string placeOfLeaving, bool pickUpFromAddress, string description, DateTime ETA)
        {
            var dbTrip = this.GetById(tripId);            

            int takenSeats = dbTrip.Passengers.Count();
            int availableSeats = takenSeats + leftAvailableSeats;
            dbTrip.AvailableSeats = availableSeats;

            dbTrip.DateOfLeaving = dateOfLeaving;
            dbTrip.PlaceOfLeaving = placeOfLeaving;
            dbTrip.PickUpFromAddress = pickUpFromAddress;
            dbTrip.Description = description;
            dbTrip.ETA = ETA;

            this.tripRepos.Save();

            return dbTrip;
        }

        public int GetAvailableLeftSeatsCount(int tripId)
        {
            var dbTrip = this.GetById(tripId);
            int leftAvailableSeats = dbTrip.AvailableSeats - dbTrip.Passengers.Count();
            return leftAvailableSeats;
        }
    }
}
