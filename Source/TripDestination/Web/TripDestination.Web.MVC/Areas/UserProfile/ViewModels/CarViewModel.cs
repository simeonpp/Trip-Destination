namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Infrastructure.HtmlSanitizer;
    using System.Collections.Generic;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class CarViewModel : IMapFrom<Car>
    {
        private readonly ISanitizer htmlSanitizer;

        public CarViewModel()
        {
            this.htmlSanitizer = new HtmlSanitizerAdapter();
        }

        public int TotalSeats { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public int? Year { get; set; }

        public SpaceForLugage SpaceForLugage { get; set; }

        public ICollection<PhotoViewModel> Photos { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.Description);
            }
        }
    }
}