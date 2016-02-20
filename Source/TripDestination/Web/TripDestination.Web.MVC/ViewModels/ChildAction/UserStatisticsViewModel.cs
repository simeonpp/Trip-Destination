namespace TripDestination.Web.MVC.ViewModels.ChildAction
{
    public class UserStatisticsViewModel
    {
        public int TotalTrips { get; set; }

        public int CommentsCount { get; set; }

        public int TripsAsDriverCount { get; set; }

        public int TripsAsPassengerCount { get; set; }
    }
}