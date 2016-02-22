namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class TripNotificationServices : ITripNotificationServices
    {
        private readonly IDbRepository<TripNotification> tripNotificationRepos;

        private readonly IDbRepository<NotificationType> notificationTypeRepos;

        public TripNotificationServices(IDbRepository<TripNotification> tripNotificationRepos, IDbRepository<NotificationType> notificationTypeRepos)
        {
            this.tripNotificationRepos = tripNotificationRepos;
            this.notificationTypeRepos = notificationTypeRepos;
        }

        public Notification GetById(int id)
        {
            return this.tripNotificationRepos
                .All()
                .Where(n => n.Id == id)
                .FirstOrDefault();
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

        public IQueryable<Notification> GetForUser(string userId)
        {
            var now = DateTime.Now;
            var notificiations = this.tripNotificationRepos
                .All()
                .Where(n => n.ForUserId == userId
                        && DbFunctions.TruncateTime(n.AvailableAfter) < now
                        && DbFunctions.TruncateTime(n.DueTo) > now
                        && n.IsDeleted == false);

            return notificiations;
        }

        public void SetAsSeen(int id)
        {
            var dbNotification = this.GetById(id);

            if (dbNotification == null)
            {
                throw new Exception("Notification not found.");
            }

            dbNotification.Seen = true;
            this.tripNotificationRepos.Save();
        }
    }
}
