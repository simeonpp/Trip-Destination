namespace TripDestination.Web.MVC.Controllers
{
    using System.Web.Mvc;
    using Data.Models;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Page;
    using System.Linq;
    using Services.Data.Contracts;

    public class PageController : BaseController
    {
        public PageController(IPageServices pageServices)
        {
            this.PageServices = pageServices;
        }

        public IPageServices PageServices { get; set; }

        [HttpGet]
        public ActionResult Index(int id)
        {
            Page page = this.PageServices.GetById(id);

            if (page == null)
            {
                return this.View("Error");
            }

            var paragraphs = this.PageServices
                .GetParagraphsByPage(page)
                .ProjectTo<PageParagraphViewModel>(this.MapperConfiguration)
                .ToList();

            PageViewModel videModel = new PageViewModel()
            {
                PageTitle = page.Heading,
                Paragraphs = paragraphs
            };

            return this.View(videModel);
        }
    }
}