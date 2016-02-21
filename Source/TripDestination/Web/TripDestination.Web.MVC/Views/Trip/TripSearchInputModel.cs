namespace TripDestination.Web.MVC.Views.Trip
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Attributes;

    public class TripSearchInputModel
    {
        [Required]
        [Display(Name = "From")]
        public int FromId { get; set; }

        [Required]
        [Display(Name = "To")]
        public int ToId { get; set; }

        [Required]
        public int Passengers { get; set; }

        [Required]
        [Display(Name = "Date of leaving")]
        public DateTime DateOfLeaving { get; set; }

        [Display(Name = "Driver name (optional)")]
        [PlaceHolder("Enter username")]
        public string DriverName { get; set; }

        [Required]
        [Display(Name = "Items per page")]
        public int ItemsPerPage { get; set; }

        [Required]
        public decimal MinPrice { get; set; }

        [Required]
        public decimal MaxPrice { get; set; }
    }
}