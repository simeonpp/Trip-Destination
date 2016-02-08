namespace TripDestination.Data.Models
{
    using Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class NotificationType
    {
        [Index]
        public int Id { get; set; }

        [Required]
        public NotificationKey Key { get; set; }

        [Required]
        [MaxLength(ModelConstants.NotificationTypeDescriptionMaxLength, ErrorMessage = "Notification type description can not be more than 500 symbols long.")]
        public string Description { get; set; }
    }
}
