namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;
    using System.Linq;

    public interface IViewServices
    {
        IQueryable<View> GetAll();

        void AddView(Trip trip, string ip);        
        
        Delete(int id);
    }
}
