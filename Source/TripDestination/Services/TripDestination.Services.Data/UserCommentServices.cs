namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class UserCommentServices : IUserCommentServices
    {
        private IDbRepository<UserComment> userCommentRepositories;

        public UserCommentServices(IDbRepository<UserComment> userCommentRepositories)
        {
            this.userCommentRepositories = userCommentRepositories;
        }

        private UserComment GetById(int id)
        {
            return this.userCommentRepositories
                .All()
                .Where(uc => uc.Id == id)
                .FirstOrDefault();
        }

        public void Delete(int id)
        {
            var userComment = this.GetById(id);

            if (userComment == null)
            {
                throw new Exception("User comment not found.");
            }

            this.userCommentRepositories.Delete(userComment);
            this.userCommentRepositories.Save();
        }

        public UserComment Edit(int id, string text)
        {
            var userComment = this.GetById(id);

            if (userComment == null)
            {
                throw new Exception("User comment not found.");
            }

            userComment.Text = text;
            this.userCommentRepositories.Save();
            return userComment;
        }

        public IQueryable<UserComment> GetAll()
        {
            return this.userCommentRepositories.All();
        }
    }
}
