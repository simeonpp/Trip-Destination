namespace TripDestination.Services.Data.Contracts
{
    using Common.Infrastructure.Models;
    using System;
    using System.Linq;
    using TripDestination.Data.Models;

    public interface ITripNotificationServices
    {
        TripNotification GetById(int id);

        IQueryable<Notification> GetForUser(string userId);

        void SetAsSeen(int id);

        BaseResponseAjaxModel ApproveNotification(int notificationId, string userId);

        BaseResponseAjaxModel DisapproveNotification(int notificationId, string userId);
    }
}
