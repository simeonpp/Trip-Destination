namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System;
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using Infrastructure.Attributes;
    public abstract class BaseTripExtentendedModel : BaseTripModel
    {
        [Required]
        [Display(Name = "Place of leaving")]
        [PlaceHolder("Enter place")]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        [Required]
        public bool PickUpFromAddress { get; set; }

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        [DataType(DataType.MultilineText)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        [Required]
        public DateTime ETA { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}