namespace TripDestination.Services.Web.Providers.Contracts
{
    using System;
    using System.Collections.Generic;
    using TripDestination.Web.Infrastructure.Models;
    using System.Linq;

    public interface IDateProvider
    {
        IQueryable<WeekDay> GetWeekAhedDays(DateTime chosenDay);

        DateTime CovertDateFromStringToDateTime(string date);
    }
}
