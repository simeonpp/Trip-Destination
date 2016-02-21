namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface ITownsServices
    {
        IQueryable<Town> GetAll();

        Town Create(string name);

        Town Edit(int id, string name);

        void Delete(int id);
    }
}
