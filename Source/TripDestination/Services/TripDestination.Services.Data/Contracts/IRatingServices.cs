namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IRatingServices
    {
        Rating GetById(int id);

        IQueryable<Rating> GetAll();

        Rating Edit(int id, int value);

        void Delete(int id);

        void RateUser(string userToRateId, string fromUserId, int rating, TripNotification tripNotification);
    }
}
