namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;

    public class View : BaseModel<int>
    {
        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
