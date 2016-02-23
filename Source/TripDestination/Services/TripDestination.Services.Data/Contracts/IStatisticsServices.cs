namespace TripDestination.Services.Data.Contracts
{

    public interface IStatisticsServices
    {
        int GetTotalTripsCount();

        string GetTopDestination();

        double GetAverateTripRating();

        int GetTripViews();

        int TripsGetTodayCreatedCount();

        int TripsGetTodayInProgressCount();

        int TripsGetTodayFinishedCount();

        string TripsGetTodayTopDestination();

        int GetUserCommentsCount(string userId);

        int GetUserTripsAsDriverCount(string userId);

        int GetUserTripsAsPassengerCount(string userId);
    }
}
