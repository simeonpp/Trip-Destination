namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using TripDestination.Data.Models;

    public class TripNotificationServices : ITripNotificationServices
    {
        public TripNotification Create(int tripId, string fromUserId, string forUserId, string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
