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
        private readonly IDbRepository<Trip> tripRepos;

        private readonly IDbRepository<UserComment> userCommentRepos;

        private readonly IDbRepository<TripComment> tripCommentRepos;

        // private IDbRepository<User> userRepos;

        private DateTime today = DateTime.Today;

        public StatisticsServices(IDbRepository<Trip> tripRepos, IDbRepository<UserComment> userCommentRepos, IDbRepository<TripComment> tripCommentRepos)
        {
            this.tripRepos = tripRepos;
            this.userCommentRepos = userCommentRepos;
            this.tripCommentRepos = tripCommentRepos;
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

        public int GetUserCommentsCount(string userId)
        {
            int tripComments = this.tripCommentRepos
                .All()
                .Where(c => c.AuthorId == userId)
                .Count();

            int userComments = this.tripCommentRepos
                .All()
                .Where(c => c.AuthorId == userId)
                .Count();

            return tripComments + userComments;
        }

        public int GetUserTripsAsDriverCount(string userId)
        {
            int result = this.tripRepos
                .All()
                .Where(t => t.DriverId == userId)
                .Count();

            return result;
        }

        public int GetUserTripsAsPassengerCount(string userId)
        {
            int result = this.tripRepos
                .All()
                .Select(t => t.Passengers.Where(p => p.UserId == userId))
                .Count();

            return result;
        }
    }
}
