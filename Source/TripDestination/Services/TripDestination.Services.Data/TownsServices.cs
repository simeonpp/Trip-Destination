namespace TripDestination.Data.Services
{
    using Common;
    using Models;
    using System.Linq;
    using TripDestination.Services.Data.Contracts;

    public class TownsServices : ITownsServices
    {
        private readonly IDbRepository<Town> townRepos;

        public TownsServices(IDbRepository<Town> townRepos)
        {
            this.townRepos = townRepos;
        }

        public IQueryable<Town> GetAll()
        {
            var towns = this.townRepos
                .All()
                .OrderBy(t => t.Name);

            return towns;
        }
    }
}
