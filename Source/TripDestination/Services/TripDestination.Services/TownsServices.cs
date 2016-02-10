namespace TripDestination.Services
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Data.Models;
    using Data.Data.Repositories;
    using System.Linq;

    public class TownsServices : ITownsServices
    {
        private IRepository<Town> townRepos;

        public TownsServices(IRepository<Town> townRepos)
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
