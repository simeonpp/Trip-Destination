﻿namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IViewServices
    {
        IQueryable<View> GetAll();

        void AddView(Trip trip, string ip);

        void Delete(int id);
    }
}
