namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;

    public class CarViewModel : IMapFrom<Car>
    {
        // TODO: IEnumerable<Photo> photos

        public int TotalSeats { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int? Year { get; set; }

        public SpaceForLugage SpaceForLugage { get; set; }

        public string Description { get; set; }
    }
}