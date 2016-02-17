namespace TripDestination.Services.Data.Models
{
    using System.Collections.Generic;

    internal class LoadedCommentsResponseModel
    {
        public IEnumerable<CommentResponseModel> Comments { get; set; }

        public bool HasMoreCommentsToLoad { get; set; }

        public int Offset { get; set; }
    }
}
