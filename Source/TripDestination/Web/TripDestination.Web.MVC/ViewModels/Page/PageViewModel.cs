namespace TripDestination.Web.MVC.ViewModels.Page
{
    using System.Collections.Generic;

    public class PageViewModel
    {
        public string PageTitle { get; set; }

        public List<PageParagraphViewModel> Paragraphs { get; set; }
    }
}