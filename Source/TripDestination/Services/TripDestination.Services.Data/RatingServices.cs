namespace TripDestination.Services.Data
{
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
    }
}
