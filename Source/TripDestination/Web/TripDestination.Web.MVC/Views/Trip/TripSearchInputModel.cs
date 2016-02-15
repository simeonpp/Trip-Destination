namespace TripDestination.Web.MVC.Views.Trip
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TripSearchInputModel
    {
        [Required]
        public int FromId { get; set; }

        [Required]
        public int ToId { get; set; }

        [Required]
        public int Passengers { get; set; }

        [Required]
        [Display(Name = "Place of leaving")]
        public DateTime DateOfLeaving { get; set; }


    }
}