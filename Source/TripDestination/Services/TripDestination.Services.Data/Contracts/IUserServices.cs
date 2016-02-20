namespace TripDestination.Services.Data.Contracts
{
    using Common.Infrastructure.Models;
    using TripDestination.Data.Models;

    public interface IUserServices
    {
        User GetById(string id);

        User GetByUsername(string username);

        string[] GetUserRoles(string id);

        BaseResponseAjaxModel AddComment(string userId, string fromUserId, string commentText);

        BaseResponseAjaxModel LoadComments(string userId, int offset);

        bool CheckIfTripHasMoreCommentsToLoad(string userId, int currentLoadedComments);

        User Update(string userId, string email, string firstName, string lastName, string phoneNumber, string description);
    }
}
