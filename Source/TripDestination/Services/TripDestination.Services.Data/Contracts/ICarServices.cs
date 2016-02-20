namespace TripDestination.Services.Data.Contracts
{
    using System.Collections.Generic;
    using TripDestination.Data.Models;

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
    }
}
