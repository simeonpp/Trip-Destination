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

        public TripServices(IDbRepository<Trip> tripRepos)
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
    }
}
