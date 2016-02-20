namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using System.Collections.Generic;
    public class CarViewModel : IMapFrom<Car>
    {
        public int TotalSeats { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int? Year { get; set; }

        public SpaceForLugage SpaceForLugage { get; set; }

        public ICollection<PhotoViewModel> Photos { get; set; }

        public string Description { get; set; }
    }
}