namespace TripDestination.Services.Data.Contracts
{
    using System.Collections.Generic;
    using TripDestination.Data.Models;
    using System.Linq;

    public interface ICarServices
    {
        Car Update(
            string userId,
            string brand,
            string model,
            string color,
            int? year,
            int totalSeats,
            SpaceForLugage luggageSpace,
            string description);

        Car Update(
            int id,
            string brand,
            string model,
            string color,
            int? year,
            int totalSeats,
            SpaceForLugage luggageSpace,
            string description);

        void Delete(int id);

        IQueryable<Car> GetAll();

    }
}
