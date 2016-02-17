namespace TripDestination.Services.Data.Models
{
    internal class CommentResponseModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserUrl { get; set; }

        public string UserImageSrc { get; set; }

        public string CreatedOnFormatted { get; set; }

        public string CommentText { get; set; }

        public int CommentTotalCount { get; set; }
    }
}
