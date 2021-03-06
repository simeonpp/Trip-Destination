﻿namespace TripDestination.Web.MVC.ViewModels.Statistics
{
    using System.Collections.Generic;
    using Data.Models;

    public class StatisticsViewModel
    {
        public int TripsCount { get; set; }

        public string TopDestination { get; set; }

        public int UsersCount { get; set; }

        public int DriversCount { get; set; }

        public string AverageTripRating { get; set; }

        public int TripViews { get; set; }

        public IEnumerable<User> TopDrivers { get; set; }

        public IEnumerable<User> TopPassengers { get; set; }
    }
}