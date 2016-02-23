namespace TripDestination.Services.Data
{
    using System;
    using System.Linq;
    using Contracts;
    using TripDestination.Data.Models;
    using TripDestination.Data.Common;

    public class ViewServices : IViewServices
    {
        private readonly IDbRepository<View> viewRepos;

        public ViewServices(IDbRepository<View> viewRepos)
        {
            this.viewRepos = viewRepos;
        }

        public void AddView(Trip trip, string ip)
        {
            bool userAlreadySeenThisTrip = trip.Views
                .Where(v => v.TripId == trip.Id && v.Ip == ip)
                .Any();

            if (!userAlreadySeenThisTrip)
            {
                View view = new View()
                {
                    TripId = trip.Id,
                    Ip = ip
                };

                this.viewRepos.Add(view);
                this.viewRepos.Save();
            }
        }

        public void Delete(int id)
        {
            var view = this.viewRepos
                .All()
                .Where(v => v.Id == id)
                .FirstOrDefault();

            if (view == null)
            {
                throw new Exception("No such view.");
            }

            this.viewRepos.Delete(view);
            this.viewRepos.Save();
        }

        public IQueryable<View> GetAll()
        {
            return this.viewRepos
                .All();
        }
    }
}
