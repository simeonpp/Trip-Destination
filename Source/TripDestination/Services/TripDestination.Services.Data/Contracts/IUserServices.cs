﻿namespace TripDestination.Services.Data.Contracts
{
    using Common.Infrastructure.Models;
    using TripDestination.Data.Models;
    using System.Linq;

    public interface IUserServices
    {
        User GetById(string id);

        IQueryable<User> GetAll();

        User CreateAdmin(string username, string email, string password, string firstName, string lastName);

        User Edit(string userId, string email, string firstName, string lastName);

        void Delete(string userID);

        User GetByUsername(string username);

        string[] GetUserRoles(string id);

        int GetUsersCountInRole(string role);

        BaseResponseAjaxModel AddComment(string userId, string fromUserId, string commentText);

        BaseResponseAjaxModel LoadComments(string userId, int offset);

        bool CheckIfTripHasMoreCommentsToLoad(string userId, int currentLoadedComments);

        User Update(string userId, string email, string firstName, string lastName, string phoneNumber, string description);

        IQueryable<UserComment> GetComments(string userId);

        int GetTotatlComments(string userId);
    }
}
