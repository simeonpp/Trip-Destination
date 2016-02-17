namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface IViewServices
    {
        void AddView(Trip trip, string ip);
    }
}
