﻿namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IPhotoServices
    {
        Photo GetByFileName(string fileName);

        IQueryable<Photo> GetAll();
    }
}
