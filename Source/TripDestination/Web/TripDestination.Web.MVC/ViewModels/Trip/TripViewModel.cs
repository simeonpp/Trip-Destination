namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;
    using Common.Infrastructure.Constants;
    using Shared;

    public class TripViewModel : BaseTripViewModel
    {
        public DateTime CreatedOn { get; set; }

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        public string Description { get; set; }

        [Required]
        public DateTime ETA { get; set; }

        [Required]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        public bool PickUpFromAddress { get; set; }

        [Required(ErrorMessage = "Please enter price.")]
        public decimal Price { get; set; }

        public TripStatus Status { get; set; }

        public double Ratings { get; set; }
    }
}