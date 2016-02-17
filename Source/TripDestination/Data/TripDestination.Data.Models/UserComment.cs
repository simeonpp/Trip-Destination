namespace TripDestination.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserComment : BaseComment
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
