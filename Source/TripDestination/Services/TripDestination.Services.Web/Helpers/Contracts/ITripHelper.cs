namespace TripDestination.Services.Web.Helpers.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface ITripHelper
    {
        IEnumerable<Tuple<string, string>> GetTopDestinations();
    }
}
