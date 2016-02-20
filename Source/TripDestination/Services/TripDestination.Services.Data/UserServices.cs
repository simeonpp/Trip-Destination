namespace TripDestination.Services.Data
{
    using Contracts;
    using System.Linq;
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
            var user = this.userRepos.All()
                .Where(u => u.Id == id)
                .FirstOrDefault();

            return user;
        }

        public User GetByUsername(string username)
        {
            var user = this.userRepos.All()
                .Where(u => u.UserName == username)
                .FirstOrDefault();

            return user;
        }
    }
}
