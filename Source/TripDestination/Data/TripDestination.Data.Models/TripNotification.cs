namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TripNotification : Notification
    {
        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public string FromUserId { get; set; }

        public virtual User FromUser { get; set; }
    }
}
