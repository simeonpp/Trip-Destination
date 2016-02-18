namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface IUserServices
    {
        User GetByUsername(string username);
    }
}
