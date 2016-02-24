namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using Common.Infrastructure.Models;
    using TripDestination.Data.Models;

    public interface ITripNotificationServices
    {
        IQueryable<TripNotification> GetAll();

        TripNotification GetById(int id);

        IQueryable<Notification> GetForUser(string userId);

        IQueryable<Notification> GetNotSeenForUser(string userId);

        void SetAsSeen(int id);

        BaseResponseAjaxModel ApproveNotification(int notificationId, string userId);

        BaseResponseAjaxModel DisapproveNotification(int notificationId, string userId);

        void SetTripFinishActionHasBeenTakenToTrue(TripNotification tripNotification);

        TripNotification GetTripFinishTripNotificationByTripAndForUser(int tripId, string userId);
    }
}
