namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class PageViewModel : IMapFrom<Page>
    {
        public int Id { get; set; }

        public string Heading { get; set; }

        public string SubHeading { get; set; }

        public string Slug { get; set; }
    }
}