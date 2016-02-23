namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface ILikeServices
    {
        Like GetById(int id);

        IQueryable<Like> GetAll();

        Like Edit(int id, bool value);

        void Delete(int id);
    }
}
