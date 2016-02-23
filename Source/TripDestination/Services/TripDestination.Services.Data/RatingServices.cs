namespace TripDestination.Services.Data
{
    using Contracts;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class RatingServices : IRatingServices
    {
        private readonly IDbRepository<Rating> ratingRepos;

        public void RateUser(string userToRateId, string fromUserId, int rating)
        {
            Rating ratingToBeSaved = new Rating()
            {
                RatedUserId = userToRateId,
                FromUserId = fromUserId,
                Value = rating
            };

            this.ratingRepos.Add(ratingToBeSaved);
            this.ratingRepos.Save();
        }
    }
}
