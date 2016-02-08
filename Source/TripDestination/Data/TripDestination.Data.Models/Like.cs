namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Like
    {
        public Like()
        {
            this.Value = false;
        }

        [Index]
        public int Id { get; set; }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public bool Value { get; set; }
    }
}
