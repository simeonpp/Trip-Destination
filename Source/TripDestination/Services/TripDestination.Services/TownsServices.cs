namespace TripDestination.Services
{
    using Contracts;
    using Data.Models;
    using System.Linq;
    using Data.Common;

    public class TownsServices : ITownsServices
    {
        private IDbRepository<Town> townRepos;

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
