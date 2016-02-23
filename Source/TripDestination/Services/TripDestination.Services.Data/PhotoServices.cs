namespace TripDestination.Services.Data
{
    using Contracts;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class PhotoServices : IPhotoServices
    {
        private readonly IDbGenericRepository<Photo, int> photoRepos;

        public PhotoServices(IDbGenericRepository<Photo, int> photoRepos)
        {
            this.photoRepos = photoRepos;
        }

        public IQueryable<Photo> GetAll()
        {
            return this.photoRepos.All();
        }

        public Photo GetByFileName(string fileName)
        {
            var photo = this.photoRepos.All()
                .Where(p => p.FileName == fileName)
                .FirstOrDefault();

            return photo;
        }
    }
}
