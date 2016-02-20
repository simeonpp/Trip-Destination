namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels.ChildAction
{
    public class UserStatisticsViewModel
    {
        public int TotalTrips { get; set; }

        public int CommentsCount { get; set; }

        public int TripsAsDriverCount { get; set; }

        public int TripsAsPassengerCount { get; set; }
    }
}