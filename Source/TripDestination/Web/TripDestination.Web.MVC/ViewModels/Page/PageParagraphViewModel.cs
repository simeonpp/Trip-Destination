namespace TripDestination.Web.MVC.ViewModels.Page
{
    using Common.Infrastructure.Mapping;
    using Data.Models;
    using Infrastructure.HtmlSanitizer;
    public class PageParagraphViewModel : IMapFrom<PageParagraph>
    {
        private readonly ISanitizer htmlSanitizer;

        public PageParagraphViewModel()
        {
            this.htmlSanitizer = new HtmlSanitizerAdapter();
        }

        public string MainHeading { get; set; }

        public string MainSubHeading { get; set; }

        public PageParagraphType Type { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public string TextSanitized
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.Text);
            }
        }

        public string AdditionalHeading { get; set; }

        public string AdditionalText { get; set; }

        public string AdditionalTextSanitized
        {
            get
            {
                return this.htmlSanitizer.Sanitize(this.AdditionalText);
            }
        }
    }
}