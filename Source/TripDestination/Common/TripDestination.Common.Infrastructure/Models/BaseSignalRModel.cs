namespace TripDestination.Common.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;

    public class BaseSignalRModel
    {
        public IEnumerable<Tuple<string, int>> UsersNotificationCounts { get; set; }
    }
}
