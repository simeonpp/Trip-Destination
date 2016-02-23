namespace TripDestination.Services.Web.Providers.Contracts
{
    using Data.Models;
    using System;
    using TripDestination.Data.Models;

    public interface INotificationProvider
    {
        NotificationAvailableActionModel GetAvailableActionModel(NotificationKey key, bool actionHasBeenTaken, int tripId, string userId);
    }
}
