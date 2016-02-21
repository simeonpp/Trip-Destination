namespace TripDestination.Data.Services
{
    using Common;
    using Models;
    using System.Linq;
    using TripDestination.Services.Data.Contracts;
    using System;

    public class TownsServices : ITownsServices
    {
        private readonly IDbRepository<Town> townRepos;

        public TownsServices(IDbRepository<Town> townRepos)
        {
            this.townRepos = townRepos;
        }

        public Town Create(string name)
        {
            Town town = new Town()
            {
                Name = name
            };

            this.townRepos.Add(town);
            this.townRepos.Save();

            return town;
        }

        public void Delete(int id)
        {
            var town = this.townRepos
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            if (town != null)
            {
                this.townRepos.Delete(town);
                this.townRepos.Save();
            }
        }

        public Town Edit(int id, string name)
        {
            var town = this.townRepos
                .All()
                .Where(t => t.Id == id)
                .FirstOrDefault();

            if (town == null)
            {
                throw new Exception("No such town.");
            }

            town.Name = name;
            this.townRepos.Save();

            return town;
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
