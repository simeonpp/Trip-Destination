namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;
    public interface INotificationTypeServices
    {
        IQueryable<NotificationType> GetAll();
    }
}
