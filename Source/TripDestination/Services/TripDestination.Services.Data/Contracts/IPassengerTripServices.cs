namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IPassengerTripServices
    {
        IQueryable<PassengerTrip> GetAll();

        void Delete(int id);
    }
}
