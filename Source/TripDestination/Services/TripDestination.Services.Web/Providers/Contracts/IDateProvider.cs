namespace TripDestination.Services.Web.Providers.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IDateProvider
    {
        IEnumerable<DateTime> GetWeekAhedDays();
    }
}
