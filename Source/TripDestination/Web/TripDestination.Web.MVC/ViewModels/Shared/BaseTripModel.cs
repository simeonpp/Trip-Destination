namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Infrastructure.Constants;
    using Data.Models;
    using Common.Infrastructure.Mapping;

    public abstract class BaseTripModel : IMapFrom<Trip>
    {
        [Required]
        [Display(Name = "From")]
        //[MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "From town name can no be less than 4 symbols long.")]
        //[MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "From town name can no be more than 50 symbols long.")]
        public int FromId { get; set; }


        [Required]
        [Display(Name = "To")]
        //[MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "From town name can no be less than 4 symbols long.")]
        //[MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "From town name can no be more than 50 symbols long.")]
        public int ToId { get; set; }

        [Required]
        [Display(Name = "Date of leaving")]
        public DateTime DateOfLeaving { get; set; }

        [Required]
        [Display(Name = "Available seats")]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int AvailableSeats { get; set; }
    }
}