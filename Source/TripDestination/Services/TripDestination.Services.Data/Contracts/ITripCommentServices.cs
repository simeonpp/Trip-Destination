namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;
    public interface ITripCommentServices
    {
        IQueryable<TripComment> GetAll();

        TripComment Edit(int id, string text);

        void Delete(int id);
    }
}
