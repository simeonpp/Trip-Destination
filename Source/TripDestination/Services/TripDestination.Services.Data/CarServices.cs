﻿namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;
    using System.Linq;

    public class CarServices : ICarServices
    {
        private readonly IDbRepository<Car> carRepos;

        public CarServices(IDbRepository<Car> carRepos)
        {
            this.carRepos = carRepos;
        }

        public Car Update(string userId, string brand, string model, string color, int? year, int totalSeats, SpaceForLugage luggageSpace, string description)
        {
            var dbCar = this.carRepos
                .All()
                .Where(c => c.OwnerId == userId)
                .FirstOrDefault();

            if (dbCar == null)
            {
                throw new Exception("Car was not found.");
            }

            dbCar.Brand = brand;
            dbCar.Model = model;
            dbCar.Color = color;
            dbCar.Year = year;
            dbCar.TotalSeats = totalSeats;
            dbCar.SpaceForLugage = luggageSpace;
            dbCar.Description = description;

            this.carRepos.Save();
            return dbCar;
        }

        public Car Update(int id, string brand, string model, string color, int? year, int totalSeats, SpaceForLugage luggageSpace, string description)
        {
            var dbCar = this.carRepos
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (dbCar == null)
            {
                throw new Exception("Car was not found.");
            }

            dbCar.Brand = brand;
            dbCar.Model = model;
            dbCar.Color = color;
            dbCar.Year = year;
            dbCar.TotalSeats = totalSeats;
            dbCar.SpaceForLugage = luggageSpace;
            dbCar.Description = description;

            this.carRepos.Save();
            return dbCar;
        }

        public void Delete(int id)
        {
            var dbCar = this.carRepos
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (dbCar == null)
            {
                throw new Exception("Car was not found.");
            }

            this.carRepos.Delete(dbCar);
        }

        public IQueryable<Car> GetAll()
        {
            return this.carRepos.All();
        }
    }
}
