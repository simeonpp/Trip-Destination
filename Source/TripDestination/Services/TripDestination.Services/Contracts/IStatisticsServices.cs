namespace TripDestination.Services.Contracts
{

    public interface IStatisticsServices
    {
        int TripsGetTodayCreatedCount();

        int TripsGetTodayInProgressCount();

        int TripsGetTodayFinishedCount();

        string TripsGetTodayTopDestination();
    }
}
