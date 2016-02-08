namespace TripDestination.Data.Models
{
    using Common.Infrastructure.Constants;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Index]
        public int Id { get; set; }

        [Required]
        public int TripId { get; set; }

        public Trip Trip { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        [MinLength(ModelConstants.CommentTextMinLength, ErrorMessage = "Comment text can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.CommenttextMaxLength, ErrorMessage = "Comment text can no be more than 1000 symbosl long,")]
        public string Text { get; set; }
    }
}
