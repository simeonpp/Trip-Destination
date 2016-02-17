namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TripComment : BaseComment
    {
        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
