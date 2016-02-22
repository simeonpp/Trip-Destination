namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;
    using System.Linq;

    public class NotificationServices : INotificationServices
    {
        private readonly IDbRepository<TripNotification> tripNotificationRepos;

        private readonly IDbRepository<NotificationType> notificationTypeRepos;

        public NotificationServices(IDbRepository<TripNotification> tripNotificationRepos, IDbRepository<NotificationType> notificationTypeRepos)
        {
            this.tripNotificationRepos = tripNotificationRepos;
            this.notificationTypeRepos = notificationTypeRepos;
        }

        public TripNotification Create(
            int tripId,
            string fromUserId,
            string forUserId,
            string title,
            string message,
            NotificationKey keyType,
            DateTime dueTo)
        {
            NotificationType type = this.notificationTypeRepos
                .All()
                .Where(t => t.Key == keyType)
                .FirstOrDefault();

            if (type == null)
            {
                throw new Exception("No such type, given key type " + keyType);
            }

            TripNotification tripNotification = new TripNotification()
            {
                TripId = tripId,
                FromUserId = fromUserId,
                ForUserId = forUserId,
                Title = title,
                Message = message,
                Seen = false,
                AvailableAfter = DateTime.Now,
                DueTo = dueTo,
                Type = type
            };

            this.tripNotificationRepos.Add(tripNotification);
            this.tripNotificationRepos.Save();
            return tripNotification;
        }

        public TripNotification Create(
            int tripId,
            string fromUserId,
            string forUserId,
            string title,
            string message,
            NotificationKey keyType,
            DateTime dueTo,
            DateTime availableAfter)
        {
            NotificationType type = this.notificationTypeRepos
                .All()
                .Where(t => t.Key == keyType)
                .FirstOrDefault();

            if (type == null)
            {
                throw new Exception("No such type, given key type " + keyType);
            }

            TripNotification tripNotification = new TripNotification()
            {
                TripId = tripId,
                FromUserId = fromUserId,
                ForUserId = forUserId,
                Title = title,
                Message = message,
                Seen = false,
                AvailableAfter = availableAfter,
                DueTo = dueTo,
                Type = type
            };

            this.tripNotificationRepos.Add(tripNotification);
            this.tripNotificationRepos.Save();
            return tripNotification;
        }
    }
}
