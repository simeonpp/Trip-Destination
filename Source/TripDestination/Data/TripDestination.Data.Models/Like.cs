namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class Like : BaseModel<int>
    {
        public Like()
        {
            this.Value = false;
        }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public bool Value { get; set; }
    }
}
