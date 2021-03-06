﻿namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class PassengerTrip : BaseModel<int>
    {
        public PassengerTrip()
        {
            this.Approved = false;
        }

        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual User User { get; set; }

        [Required]
        public bool Approved { get; set; }
    }
}
