namespace TripDestination.Services.Contracts
{

    public interface IStatisticsServices
    {
        int GetTotalTripsCount();

        string GetTopDestination();

        int GetUserCount();

        int GetDriversCount();

        double GetAverateTripRating();

        int GetTripViews();

        int TripsGetTodayCreatedCount();

        int TripsGetTodayInProgressCount();

        int TripsGetTodayFinishedCount();

        string TripsGetTodayTopDestination();
    }
}
