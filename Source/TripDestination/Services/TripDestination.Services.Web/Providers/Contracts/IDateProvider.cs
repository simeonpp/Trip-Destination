namespace TripDestination.Services.Web.Providers.Contracts
{
    using System;
    using System.Collections.Generic;
    using TripDestination.Web.Infrastructure.Models;

    public interface IDateProvider
    {
        IEnumerable<WeekDay> GetWeekAhedDays(DateTime chosenDay);
    }
}
