namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class UserServices : IUserServices
    {
        private readonly IDbGenericRepository<User, string> userRepos;

        public UserServices(IDbGenericRepository<User, string> userRepos)
        {
            this.userRepos = userRepos;
        }


        public User GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
