namespace TripDestination.Data.Services
{
    using System;
    using Common;
    using Models;
    using System.Data.Entity;
    using System.Linq;
    using TripDestination.Services.Data.Contracts;

    public class StatisticsServices : IStatisticsServices
    {
        private readonly IDbRepository<Trip> tripRepos;

        private readonly IDbRepository<UserComment> userCommentRepos;

        private readonly IDbRepository<TripComment> tripCommentRepos;

        private readonly IDbRepository<View> viewRepos;

        private readonly IDbRepository<Rating> ratingRepos;

        private readonly IDbRepository<PassengerTrip> passengerTripRepos;

        private IUserServices userServices;

        private DateTime today = DateTime.Today;

        public StatisticsServices(
            IDbRepository<Trip> tripRepos,
            IDbRepository<UserComment> userCommentRepos,
            IDbRepository<TripComment> tripCommentRepos,
            IUserServices userServices,
            IDbRepository<View> viewRepos,
            IDbRepository<Rating> ratingRepos,
            IDbRepository<PassengerTrip> passengerTripRepos)
        {
            this.tripRepos = tripRepos;
            this.userCommentRepos = userCommentRepos;
            this.tripCommentRepos = tripCommentRepos;
            this.userServices = userServices;
            this.viewRepos = viewRepos;
            this.ratingRepos = ratingRepos;
            this.passengerTripRepos = passengerTripRepos;
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

        public double GetAverateTripRating()
        {
            bool hasRatings = this.ratingRepos
                .All()
                .Where(r => r.IsDeleted == false)
                .Any();

            double rating = 0.0;
            if (hasRatings)
            {
                rating = this.ratingRepos
                .All()
                .Where(r => r.IsDeleted == false)
                .Average(r => r.Value);
            }

            return rating;
        }

        public int GetTripViews()
        {
            int viewsCount = this.viewRepos
                .All()
                .Where(v => v.IsDeleted == false)
                .Count();

            return viewsCount;
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
                return string.Empty;
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
            int result = this.passengerTripRepos
                .All()
                .Where(p => p.UserId == userId)
                .Count();

            return result;
        }

        public int GetRatingForUser(string userId)
        {
            bool hasAny = this.ratingRepos
                .All()
                .Where(r => r.RatedUserId == userId && r.IsDeleted == false)
                .Any();

            if (hasAny)
            {
                double rating = this.ratingRepos
                .All()
                .Where(r => r.RatedUserId == userId && r.IsDeleted == false)
                .Average(r => r.Value);
                return (int)Math.Round(rating);
            }

            return 0;
        }
    }
}
