namespace TripDestination.Services
{
    using Common.Infrastructure.Constants;
    using Contracts;
    using Data.Data;
    using Data.Data.Repositories;
    using Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StatisticsServices : IStatisticsServices
    {
        private IRepository<Trip> tripRepos;

        private IRepository<User> userRepos;

        private ITripDestinationDbContext tripDestinationDbContext;

        private DateTime today = DateTime.Today;

        public StatisticsServices(IRepository<Trip> tripRepos, IRepository<User> userRepos, ITripDestinationDbContext TripDestinationDbContext)
        {
            this.tripRepos = tripRepos;
            this.userRepos = userRepos;
            this.tripDestinationDbContext = TripDestinationDbContext;
        }

#region general stats#
        public int GetTotalTripsCount()
        {
            int count = this.tripRepos
                .All()
                .Count();

            return count;
        }

        public string GetTopDestination()
        {
            var town = tripRepos
               .All()
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

        public int GetUserCount()
        {
            int count = this.userRepos
                .All()
                .Count();

            return count;
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
#endregion

        #region today stats #
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
#endregion#
    }
}
