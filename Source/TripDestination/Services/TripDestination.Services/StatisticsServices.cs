namespace TripDestination.Services
{
    using Contracts;
    using Data.Data.Repositories;
    using Data.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StatisticsServices : IStatisticsServices
    {
        private IRepository<Trip> tripRepos;

        private DateTime today = DateTime.Today;

        public StatisticsServices(IRepository<Trip> tripRepos)
        {
            this.tripRepos = tripRepos;
        }

        public int TripsGetTodayCreatedCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.CreatedOn) == this.today)
                .Count();

            return count;
        }

        public int TripsGetTodayFinishedCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.Finished)
                .Count();

            return count;
        }

        public int TripsGetTodayInProgressCount()
        {
            int count = tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.InProgress)
                .Count();

            return count;
        }

        public string TripsGetTodayTopDestination()
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
