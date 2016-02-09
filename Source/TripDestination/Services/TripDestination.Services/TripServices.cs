namespace TripDestination.Services
{
    using System;
    using Contracts;
    using Data.Models;
    using Data.Data.Repositories;
    using System.Linq;
    using System.Data.Entity;

    public class TripServices : ITripServices
    {
        private IRepository<Trip> tripRepos;

        private DateTime today = DateTime.Today;

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

        public int GetTodayCreatedCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.CreatedOn) == this.today)
                .Count();

            return count;
        }

        public int GetTodayFinishedCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.Finished)
                .Count();

            return count;
        }

        public int GetTodayInProgressCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.InProgress)
                .Count();

            return count;
        }

        public string GetTodayTopDestination()
        {
            var town = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today)
                .GroupBy(t => t.To.Name)
                .Select(group => new { TownName = group.Key, Count = group.Count() })
                .OrderByDescending(e => e.Count)
                .FirstOrDefault();

            if (town == null)
            {
                return String.Empty;
            }

            string townAsString = town.TownName;
            return townAsString;
        }
    }
}
