namespace TripDestination.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Models;
    using TripDestination.Common.Infrastructure.Constants;

    public class Notification : BaseModel<int>
    {
        public Notification()
        {
            this.Seen = false;
            this.CreatedOn = DateTime.Now;
            this.AvailableAfter = DateTime.Now;
            this.DueTo = DateTime.Now.AddDays(15);
        }

        [Required]
        public NotificationType Type { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public bool Seen { get; set; }

        [Required]
        [MinLength(ModelConstants.NotificationTitleMinLength, ErrorMessage = "Notification title can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.NotificationTitleMaxLength, ErrorMessage = "Notification title can no be more than 150 symbols long.")]
        public string Title { get; set; }

        [Required]
        [MinLength(ModelConstants.NotificationMessageMinLength, ErrorMessage = "Notification message can no be less than 10 symbols long.")]
        [MaxLength(ModelConstants.NotificationMessageMaxLength, ErrorMessage = "Notification message can no be more than 500 symbols long.")]
        public string Message { get; set; }
        
        [Required]
        public DateTime AvailableAfter { get; set; }

        [Required]
        public DateTime DueTo { get; set; }
    }
}
