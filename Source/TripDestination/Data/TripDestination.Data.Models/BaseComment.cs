namespace TripDestination.Data.Models
{
    using Common.Models;
    using System.ComponentModel.DataAnnotations;
    using TripDestination.Common.Infrastructure.Constants;


    public abstract class BaseComment : BaseModel<int>
    {
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        [MinLength(ModelConstants.CommentTextMinLength, ErrorMessage = "Comment text can no be less than 5 symbols long.")]
        [MaxLength(ModelConstants.CommentTextMaxLength, ErrorMessage = "Comment text can no be more than 1000 symbosl long,")]
        public string Text { get; set; }
    }
}
