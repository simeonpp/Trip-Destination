namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Common.Infrastructure.Constants;
    using Data.Models;
    public abstract class BaseTripModel
    {       
        [Required]
        //[MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "From town name can no be less than 4 symbols long.")]
        //[MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "From town name can no be more than 50 symbols long.")]
        public int FromId { get; set; }


        [Required]
        //[MinLength(ModelConstants.TownNameMinLength, ErrorMessage = "From town name can no be less than 4 symbols long.")]
        //[MaxLength(ModelConstants.TownNameMaxLength, ErrorMessage = "From town name can no be more than 50 symbols long.")]
        public int ToId { get; set; }

        [Required]
        public DateTime DateOfLeaving { get; set; }       

        [Required]
        [Range(ModelConstants.TripAvailableSeatsMin, ModelConstants.TripAvailableSeatsMax, ErrorMessage = "Trip available seats should be between 1 and 5")]
        public int AvailableSeats { get; set; }
    }
}