namespace TripDestination.Data.Models
{
    using Common.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PassengerTrip : BaseModel<int>
    {
        public PassengerTrip()
        {
            this.Approved = false;
        }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public bool Approved { get; set; }
    }
}
