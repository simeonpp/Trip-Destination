namespace TripDestination.Services.Web.Providers
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using System.Linq;
    using TripDestination.Web.Infrastructure.Models;

    public class DateProvider : IDateProvider
    {
        public IQueryable<WeekDay> GetWeekAhedDays(DateTime chosenDay)
        {
            var today = DateTime.Today;
            var endDate = today.AddDays(7);
            var numDays = (int)(endDate - today).TotalDays;
            var dates = Enumerable
                       .Range(0, numDays)
                       .Select(x => new WeekDay
                       {
                           Datetime = today.AddDays(x),
                           IsActive = chosenDay == today.AddDays(x)
                       })
                       .AsQueryable();

            return dates;
        }

        public DateTime CovertDateFromStringToDateTime(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                var dateParts = date.Split(new char[] { '-' });
                if (dateParts.Length == 3)
                {
                    int year, month, day;
                    bool yearIsParsable = int.TryParse(dateParts[0], out year);
                    bool mongthIsParsable = int.TryParse(dateParts[1], out month);
                    bool dayIsParsable = int.TryParse(dateParts[2], out day);

                    if (yearIsParsable && mongthIsParsable && dayIsParsable)
                    {
                        return new DateTime(year, month, day);
                    }
                }
            }

            return DateTime.Today;
        }
    }
}