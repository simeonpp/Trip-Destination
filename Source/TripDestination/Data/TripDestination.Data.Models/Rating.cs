namespace TripDestination.Data.Models
{
    using Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Rating
    {
        [Index]
        public int Id { get; set; }

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
