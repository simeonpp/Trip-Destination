namespace TripDestination.Services.Contracts
{
    using Data.Models;
    using System.Collections.Generic;
    using System.Linq;

    public interface ITownsServices
    {
        IQueryable<Town> GetAll();
    }
}
