namespace TripDestination.Data.Models
{
    using Common.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;

    public class Trip : BaseModel<int>
    {
        private HashSet<Rating> ratings;

        private HashSet<Comment> comments;

        private HashSet<Like> likes;

        private HashSet<View> views;

        public Trip()
        {
            this.ratings = new HashSet<Rating>();
            this.comments = new HashSet<Comment>();
            this.likes = new HashSet<Like>();
            this.views = new HashSet<View>();
            
            this.PickUpFromAddress = false;
            this.Status = TripStatus.Open;
            this.Price = 0;
        }

        [Required]
        public DateTime DateOfLeaving { get; set; }

        public int FromId { get; set; }

        public virtual Town From { get; set; }

        public int ToId { get; set; }

        public virtual Town To { get; set; }

        public string DriverId { get; set; }

        public User Driver { get; set; }
        
        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        [Required]
        public DateTime ETA { get; set; }

        [Required]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        [Required]
        public bool PickUpFromAddress { get; set; }

        [Required]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int AvailableSeats { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public TripStatus Status { get; set; }

        public virtual HashSet<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }

        public virtual HashSet<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual HashSet<Like> Likes
        {
            get { return this.likes; }
            set { this.likes = value; }
        }

        public virtual HashSet<View> Views
        {
            get { return this.views; }
            set { this.views = value; }
        }
    }
}