namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Collections.Generic;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class CarServices : ICarServices
    {
        private readonly IDbRepository<Car> carRepos;

        public CarServices(IDbRepository<Car> carRepos)
        {
            this.carRepos = carRepos;
        }

        public Car Create(string ownerId, string brand, string model, int totalSeats, string color, int year, SpaceForLugage luggageSpace, string description, IEnumerable<Photo> photos)
        {
            var car = new Car()
            {
                OwnerId = ownerId,
                Brand = brand,
                Model = model,
                TotalSeats = totalSeats,
                Color = color,
                Year = year,
                SpaceForLugage = luggageSpace,
                Description = description
            };

            this.carRepos.Add(car);

            var dbContext = this.carRepos.Context;

            this.carRepos.Save();
            return car;
        }
    }
}
