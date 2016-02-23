namespace TripDestination.Services.Data
{
    using System;
    using Contracts;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using Common.Infrastructure.Models;

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

        public BaseSignalRModel SendNotifications(IEnumerable<string> userIds)
        {
            var tuplesList = new List<Tuple<string, int>>();
            var now = DateTime.Now;
            foreach (var userId in userIds)
            {
                int notSeenNotificationsCount = this.tripNotificationRepos
                   .All()
                   .Where(n => n.ForUserId == userId
                           && DbFunctions.TruncateTime(n.AvailableAfter) < now
                           /*&& DbFunctions.TruncateTime(n.DueTo) > now*/
                           && n.IsDeleted == false
                           && n.Seen == false)
                   .Count();

                tuplesList.Add(new Tuple<string, int>(userId, notSeenNotificationsCount));
            }

            var signalRModel = new BaseSignalRModel()
            {
                UsersNotificationCounts = tuplesList
            };

            return signalRModel;
        }
    }
}
