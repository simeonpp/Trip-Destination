namespace TripDestination.Web.MVC.Areas.Admin.ViewModels
{
    using Common.Infrastructure.Constants;
    using System;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class TripAdminViewModel : IMapFrom<Trip>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateOfLeaving { get; set; }

        [Required]
        public int FromId { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
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

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        public string Description { get; set; }
    }
}