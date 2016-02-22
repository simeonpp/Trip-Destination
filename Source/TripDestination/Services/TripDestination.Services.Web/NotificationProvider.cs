namespace TripDestination.Services.Web
{
    using Providers.Contracts;
    using System;
    using Data.Models;
    using TripDestination.Data.Models;
    using Data.Contracts;
    public class NotificationProvider : INotificationProvider
    {
        private readonly ITripServices tripServices;

        public NotificationProvider(ITripServices tripServices)
        {
            this.tripServices = tripServices;
        }

        public NotificationAvailableActionModel GetAvailableActionModel(NotificationKey key, bool actionHasBeenTaken, int tripId, string userId)
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
                        bool tripRequestIsRejected = this.tripServices.CheckIfUserIsJoinedTrip(tripId, userId);
                        if (tripRequestIsRejected)
                        {
                            result.CanApprove = true;
                            result.CanDisapprove = true;
                        }
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
