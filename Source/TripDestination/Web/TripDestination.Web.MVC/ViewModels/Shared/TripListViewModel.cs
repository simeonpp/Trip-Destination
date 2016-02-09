namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using TripDestination.Data.Models;

    public class TripListViewModel : BaseTripViewModel
    {
        public Town From { get; set; }

        public Town To { get; set; }
    }
}