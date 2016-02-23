namespace TripDestination.Services.Data.Contracts
{
    using Common.Infrastructure.Models;
    using System;
    using System.Collections.Generic;
    using TripDestination.Data.Models;

    public interface INotificationServices
    {
        TripNotification Create(
            int tripId,
            string fromUserId,
            string forUserId,
            string title,
            string message,
            NotificationKey keyType,
            DateTime dueTo);

        TripNotification Create(
            int tripId,
            string fromUserId,
            string forUserId,
            string title,
            string message,
            NotificationKey keyType,
            DateTime dueTo,
            DateTime availableAfter);

        BaseSignalRModel SendNotifications(IEnumerable<string> userIds);
    }
}
