namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class LikeServices : ILikeServices
    {
        private readonly IDbRepository<Like> likeRepos;

        public LikeServices(IDbRepository<Like> likeRepos)
        {
            this.likeRepos = likeRepos;
        }

        public Like GetById(int id)
        {
            var like = this.likeRepos
                .All()
                .Where(l => l.Id == id)
                .FirstOrDefault();

            return like;
        }

        public IQueryable<Like> GetAll()
        {
            return this.likeRepos.All();
        }

        public Like Edit(int id, bool value)
        {
            var like = this.GetById(id);

            if (like == null)
            {
                throw new Exception("Like not found.");
            }

            like.Value = value;
            this.likeRepos.Save();
            return like;
        }

        public void Delete(int id)
        {
            var like = this.GetById(id);

            if (like == null)
            {
                throw new Exception("Like not found.");
            }

            this.likeRepos.Delete(like);
            this.likeRepos.Save();
        }
    }
}
