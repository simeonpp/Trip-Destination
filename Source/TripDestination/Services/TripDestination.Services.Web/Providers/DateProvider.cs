﻿namespace TripDestination.Services.Web.Providers
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using System.Linq;
    using TripDestination.Web.Infrastructure.Models;
    public class DateProvider : IDateProvider
    {
        public IEnumerable<WeekDay> GetWeekAhedDays(DateTime chosenDay)
        {
            var today = DateTime.Today;
            var endDate = today.AddDays(7);
            var numDays = (int)(endDate - today).TotalDays;
            List<WeekDay> dates = Enumerable
                       .Range(0, numDays)
                       .Select(x => new WeekDay
                       {
                           Datetime = today.AddDays(x),
                           IsActive = chosenDay.Day == x
                       })
                       .ToList();

            return dates;
        }
    }
}