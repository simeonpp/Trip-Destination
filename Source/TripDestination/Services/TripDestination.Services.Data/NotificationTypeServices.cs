namespace TripDestination.Services.Data
{
    using Contracts;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;
    using System;

    public class NotificationTypeServices : INotificationTypeServices
    {
        private readonly IDbRepository<NotificationType> notificationTypeRepos;

        public NotificationTypeServices(IDbRepository<NotificationType> notificationTypeRepos)
        {
            this.notificationTypeRepos = notificationTypeRepos;
        }

        public IQueryable<NotificationType> GetAll()
        {
            return this.notificationTypeRepos.All();
        }
    }
}
