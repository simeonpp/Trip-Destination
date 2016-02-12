namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;

    public class Rating : BaseModel<int>
    {
        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [Range(ModelConstants.RatingValueMin, ModelConstants.RatingValueMax, ErrorMessage = "Rating must be between 0.0 and 5.0.")]
        public double Value { get; set; }
    }
}
