namespace TripDestination.Services.Data.Models
{
    internal class ApprovedJoinRequestResponseModel
    {
        public int PassengersCount { get; set; }

        public int AvailableSeatsCount { get; set; }

        public int PendingApproveUsersCount { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageSrc { get; set; }

        public string UserProfileLink { get; set; }
    }
}
