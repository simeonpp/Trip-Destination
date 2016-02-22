namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class NotificationTypeViewModel : IMapFrom<NotificationType>
    {
        public NotificationKey Key { get; set; }

        public string Description { get; set; }
    }
}