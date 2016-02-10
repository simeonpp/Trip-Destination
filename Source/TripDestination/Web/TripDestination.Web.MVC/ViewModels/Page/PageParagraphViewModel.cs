namespace TripDestination.Web.MVC.ViewModels.Page
{
    using Common.Infrastructure.Mapping;
    using Data.Models;

    public class PageParagraphViewModel : IMapFrom<PageParagraph>
    {
        public string MainHeading { get; set; }

        public string MainSubHeading { get; set; }

        public PageParagraphType Type { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public string AdditionalHeading { get; set; }

        public string AdditionalText { get; set; }
    }
}