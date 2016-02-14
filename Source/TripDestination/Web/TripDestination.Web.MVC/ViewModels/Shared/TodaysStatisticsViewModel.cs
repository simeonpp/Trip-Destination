namespace TripDestination.Web.MVC.ViewModels.Shared
{
    public class TodaysStatisticsViewModel
    {
        public int ColMdValue { get; set; }

        public int TodayCreatedTrips { get; set; }

        public int TodayInProgressTrips { get; set; }

        public int TodayFinishedTrips { get; set; }

        public string TodayTopDestinationTown { get; set; }
    }
}