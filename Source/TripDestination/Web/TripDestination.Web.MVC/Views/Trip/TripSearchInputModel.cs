namespace TripDestination.Web.MVC.Views.Trip
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Data.Models;
    using Infrastructure.Attributes;

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

        [Display(Name = "Driver name (optional)")]
        [PlaceHolder("Enter username")]
        public string DriverName { get; set; }

        [Required]
        public SpaceForLugage LuggaeSpcace { get; set; }

        [Required]
        public int ItemsPerPage { get; set; }

        [Required]
        public decimal MinPrice { get; set; }

        [Required]
        public decimal MaxPrice { get; set; }
    }
}