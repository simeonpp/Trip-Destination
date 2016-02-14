namespace TripDestination.Data.Services
{
    using Common;
    using Data;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Services.Data.Contracts;

    public class StatisticsServices : IStatisticsServices
    {
        private IDbRepository<Trip> tripRepos;

        // private IDbRepository<User> userRepos;

        private DateTime today = DateTime.Today;

        public StatisticsServices(IDbRepository<Trip> tripRepos)
        {
            this.tripRepos = tripRepos;
            // this.userRepos = userRepos;
        }

        public int GetTotalTripsCount()
        {
            int count = this.tripRepos
                .All()
                .Count();

            return count;
        }

        public string GetTopDestination()
        {
            var town = this.tripRepos
               .All()
               .GroupBy(t => t.To.Name)
               .Select(group => new { TownName = group.Key, Count = group.Count() })
               .OrderByDescending(e => e.Count)
               .FirstOrDefault();

            if (town == null)
            {
                return string.Empty;
            }

            string townAsString = town.TownName;
            return townAsString;
        }

        public int GetUserCount()
        {            
            // int count = this.userRepos
            //    .All()
            //    .Count();

            // return count;

            throw new NotImplementedException();
        }

        public int GetDriversCount()
        {
            // TODO: Refactor this to use ITripDestinationDbContext
            var asd = new TripDestinationDbContext();

            var roleStore = new RoleStore<IdentityRole>(asd);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            int count = roleManager.FindByName(RoleConstants.AdminRole).Users.Count;
            return count;
        }

        public double GetAverateTripRating()
        {
            double rating = this.tripRepos
                .All()
                .Select(t => t.Ratings.Average(r => r.Value))
                .First();

            return rating;
        }

        public int GetTripViews()
        {
            int views = this.tripRepos
                .All()
                .Select(t => t.Views.Count())
                .First();

            return views;
        }

        public int TripsGetTodayCreatedCount()
        {
            int count = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.CreatedOn) == this.today)
                .Count();

            return count;
        }

        public int TripsGetTodayFinishedCount()
        {
            int count = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.Finished)
                .Count();

            return count;
        }

        public int TripsGetTodayInProgressCount()
        {
            int count = this.tripRepos
                .All()
                .Where(t => DbFunctions.TruncateTime(t.DateOfLeaving) == this.today && t.Status == TripStatus.InProgress)
                .Count();

            return count;
        }

        public string TripsGetTodayTopDestination()
        {
            var town = this.tripRepos
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
