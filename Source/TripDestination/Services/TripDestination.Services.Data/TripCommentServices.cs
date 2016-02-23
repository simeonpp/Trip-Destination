namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    class TripCommentServices : ITripCommentServices
    {
        private IDbRepository<TripComment> trimpCommentRepositories;

        public TripCommentServices(IDbRepository<TripComment> trimpCommentRepositories)
        {
            this.trimpCommentRepositories = trimpCommentRepositories;
        }

        public void Delete(int id)
        {
            var tripComment = this.GetById(id);

            if (tripComment == null)
            {
                throw new Exception("Tripm comment not found.");
            }

            this.trimpCommentRepositories.Delete(tripComment);
            this.trimpCommentRepositories.Save();
        }

        public TripComment Edit(int id, string text)
        {
            var tripComment = this.GetById(id);

            if (tripComment == null)
            {
                throw new Exception("User comment not found.");
            }

            tripComment.Text = text;
            this.trimpCommentRepositories.Save();
            return tripComment;
        }

        public IQueryable<TripComment> GetAll()
        {
            return this.trimpCommentRepositories.All();
        }

        private TripComment GetById(int id)
        {
            return this.trimpCommentRepositories
                .All()
                .Where(uc => uc.Id == id)
                .FirstOrDefault();
        }
    }
}
