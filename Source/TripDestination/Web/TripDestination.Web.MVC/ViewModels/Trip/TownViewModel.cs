namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Mapping;
    using Data.Models;

    public class TownViewModel : IMapFrom<Town>
    {
        public string Name { get; set; }
    }
}