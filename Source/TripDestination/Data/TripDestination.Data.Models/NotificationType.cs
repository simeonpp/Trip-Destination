namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TripDestination.Common.Infrastructure.Constants;

    public class NotificationType : BaseModel<int>
    {
        [Required]
        public NotificationKey Key { get; set; }

        [Required]
        [MaxLength(ModelConstants.NotificationTypeDescriptionMaxLength, ErrorMessage = "Notification type description can not be more than 500 symbols long.")]
        public string Description { get; set; }
    }
}
