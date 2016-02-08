namespace TripDestination.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PassengerTrip
    {
        public PassengerTrip()
        {
            this.Approved = false;
            this.RequestDate = DateTime.Now;
        }

        [Index]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public bool Approved { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }
    }
}
