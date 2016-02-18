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
    }
}
