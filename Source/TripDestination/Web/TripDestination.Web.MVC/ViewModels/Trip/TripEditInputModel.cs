namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Constants;
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TripEditInputModel : IMapFrom<Trip>
    {
        public int Id { get; set; }

        public Town From { get; set; }

        public Town To { get; set; }

        [Required]
        [MinLength(ModelConstants.TripPlaceOfLeavingMinLength, ErrorMessage = "Trip place of leaving can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.TripPlaceOfLeavingMaxLength, ErrorMessage = "Trip place of leaving can no be more than 200 symbols long.")]
        public string PlaceOfLeaving { get; set; }

        [Required]
        [Display(Name = "Left available seats")]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int LeftAvailableSeats { get; set; }

        [Required]
        [EnumDataType(typeof(SpaceForLugage))]
        public int LuggageSpace { get; set; }

        [Required]
        public DateTime DateOfLeaving { get; set; }

        [Required]
        public DateTime ETA { get; set; }

        [Required]
        public bool PickUpFromAddress { get; set; }

        [MinLength(ModelConstants.TripDescriptionMinLength, ErrorMessage = "Trip description can not be less than 10 symbols long.")]
        [MaxLength(ModelConstants.TripDescriptionMaxLength, ErrorMessage = "Trip description can not be more than 1000 symbols long.")]
        [DataType(DataType.MultilineText)]
        [UIHint("TextArea")]
        public string Description { get; set; }

        public IEnumerable<int> UsernamesToBeRemoved { get; set; }
    }
}