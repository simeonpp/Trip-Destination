namespace TripDestination.Services.Data.Contracts
{
    using System.Collections.Generic;
    using TripDestination.Data.Models;

    public interface ICarServices
    {
        Car Create(
            string ownerId,
            string brand,
            string model,
            int totalSeats,
            string color,
            int year,
            SpaceForLugage luggageSpace,
            string description,
            IEnumerable<Photo> photos
        );
    }
}
