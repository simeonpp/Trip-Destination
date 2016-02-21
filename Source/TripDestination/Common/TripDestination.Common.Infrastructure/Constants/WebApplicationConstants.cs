namespace TripDestination.Common.Infrastructure.Constants
{
    public class WebApplicationConstants
    {
        public const int HomepageTripsPerSectionCount = 4;

        public const string TempDataKeyForContactForm = "ContactFormSuccessfullySend";

        public const int CommentsOfset = 3;

        public const int ImageQuality = 70;
        public const int ImageUserAvatarNormalWidth = 303;
        public const int ImageUserAvatarSmallWidth = 85;
        public const int ImageCarWidth = 482;
        public const int ImageMaxSizeInBytes = 1572864; // 1.5MB
        public const string ImageFolder = "~/App_Data/uploads";
        public const string ImageRouteUrl = "Media/Image";

        public const int MaxItemsPerPage = 24;

        // Ordering
        public const string SortByDateOfLeaving = "DateOfLeaving";
        public const string SortByDestinationTo = "ToId";
        public const string SortByDestinationFrom = "FromId";
        public const string SortByAvailableSeats = "AvailableSeats";
        public const string SortByPrice = "Price";
        public const string SortByDriverName = "Driver";
    }
}
