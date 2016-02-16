namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface IUserServices
    {
        User GetById(string id);
    }
}
