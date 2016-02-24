namespace TripDestination.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class RatingServices : IRatingServices
    {
        private readonly IDbRepository<Rating> ratingRepos;

        private readonly ITripNotificationServices tipNotificationServices;

        public RatingServices(IDbRepository<Rating> ratingRepos, ITripNotificationServices tipNotificationServices)
        {
            this.ratingRepos = ratingRepos;
            this.tipNotificationServices = tipNotificationServices;
        }

        public Rating GetById(int id)
        {
            return this.ratingRepos
                .All()
                .Where(r => r.Id == id)
               .FirstOrDefault();
        }

        public IQueryable<Rating> GetAll()
        {
            return this.ratingRepos.All();
        }

        public Rating Edit(int id, int value)
        {
            var rating = this.GetById(id);

            if (rating == null)
            {
                throw new Exception("Rating not found.");
            }

            rating.Value = value;
            this.ratingRepos.Save();
            return rating;
        }

        public void RateUser(string userToRateId, string fromUserId, int rating, TripNotification tripNotification)
        {
            Rating ratingToBeSaved = new Rating()
            {
                RatedUserId = userToRateId,
                FromUserId = fromUserId,
                Value = rating,
            };

            this.ratingRepos.Add(ratingToBeSaved);
            this.ratingRepos.Save();

            this.tipNotificationServices.SetTripFinishActionHasBeenTakenToTrue(tripNotification);
        }

        public void Delete(int id)
        {
            var rating = this.GetById(id);

            if (rating == null)
            {
                throw new Exception("Rating not found.");
            }

            this.ratingRepos.Delete(rating);
            this.ratingRepos.Save();
        }
    }
}
