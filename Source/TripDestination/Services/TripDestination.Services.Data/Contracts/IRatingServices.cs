namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface IRatingServices
    {
        void RateUser(string userToRateId, string fromUserId, int rating, TripNotification tripNotification);
    }
}
