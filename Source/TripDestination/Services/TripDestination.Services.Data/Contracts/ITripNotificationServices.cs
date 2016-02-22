namespace TripDestination.Services.Data.Contracts
{
    using System;
    using System.Linq;
    using TripDestination.Data.Models;

    public interface ITripNotificationServices
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

        IQueryable<Notification> GetForUser(string userId);
    }
}
