namespace TripDestination.Services.Web.Providers
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using System.Linq;

    public class DateProvider : IDateProvider
    {
        public IEnumerable<DateTime> GetWeekAhedDays()
        {
            var today = DateTime.Today;
            var endDate = today.AddDays(7);
            var numDays = (int)(endDate - today).TotalDays;
            List<DateTime> dates = Enumerable
                       .Range(0, numDays)
                       .Select(x => today.AddDays(x))
                       .ToList();

            return dates;
        }
    }
}
