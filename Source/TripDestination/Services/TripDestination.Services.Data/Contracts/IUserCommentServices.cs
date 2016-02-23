namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IUserCommentServices
    {
        IQueryable<UserComment> GetAll();

        UserComment Edit(int id, string text);

        void Delete(int id);
    }
}
