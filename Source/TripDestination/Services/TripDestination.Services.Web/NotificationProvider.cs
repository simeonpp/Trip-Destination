namespace TripDestination.Services.Web
{
    using Providers.Contracts;
    using System;
    using Data.Models;
    using TripDestination.Data.Models;

    public class NotificationProvider : INotificationProvider
    {
        public NotificationAvailableActionModel GetAvailableActionModel(NotificationKey key, bool actionHasBeenTaken)
        {
            var result = new NotificationAvailableActionModel
            {
                CanApprove = false,
                CanDisapprove = false
            };

            if (!actionHasBeenTaken)
            {
                switch (key)
                {
                    case NotificationKey.AdminRoleRequest:
                        break;
                    case NotificationKey.ApprovelAdminRequest:
                        break;
                    case NotificationKey.RejectAdminRequest:
                        break;
                    case NotificationKey.JoinTripRequest:
                        result.CanApprove = true;
                        result.CanDisapprove = true;
                        break;
                    case NotificationKey.JoinTripApproved:
                        break;
                    case NotificationKey.JoinTripDisApproved:
                        break;
                    case NotificationKey.FinishTripDriverRequest:
                        result.CanApprove = true;
                        break;
                    case NotificationKey.TripFinished:
                        break;
                    case NotificationKey.TripChanged:
                        break;
                    case NotificationKey.DriverRemovePassenger:
                        break;
                    case NotificationKey.PassengerLeftTrip:
                        break;
                    default:
                        break;
                }
            }

            return result;
        }
    }
}
