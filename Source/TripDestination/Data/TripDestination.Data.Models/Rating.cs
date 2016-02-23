namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;

    public class Rating : BaseModel<int>
    {
        public string FromUserId { get; set; }

        public User FromUser { get; set; }

        public string RatedUserId { get; set; }

        public User RatedUser { get; set; }

        [Required]
        [Range(ModelConstants.RatingValueMin, ModelConstants.RatingValueMax, ErrorMessage = "Rating must be between 0.0 and 5.0.")]
        public double Value { get; set; }
    }
}
