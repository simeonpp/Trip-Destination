namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Web.MVC.Areas.Admin.ViewModels;
    using TripDestination.Common.Infrastructure.Mapping;


    public class PageParagraphAdminController : Controller
    {
        private readonly IPageParagraphServices pageParagraphServices;

        public PageParagraphAdminController(IPageParagraphServices pageParagraphServices)
        {
            this.pageParagraphServices = pageParagraphServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult PageParagraphs_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.pageParagraphServices
                .GetAll()
                .To<PageParagraphViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PageParagraphs_Create([DataSourceRequest]DataSourceRequest request, PageParagraphViewModel pageParagraph)
        {
            if (this.ModelState.IsValid)
            {
                var dbPageParagraph = this.pageParagraphServices.Create(
                    pageParagraph.PageId,
                    pageParagraph.MainHeading,
                    pageParagraph.MainSubHeading,
                    pageParagraph.Type,
                    pageParagraph.Heading,
                    pageParagraph.Text,
                    pageParagraph.AdditionalHeading,
                    pageParagraph.AdditionalText);

                pageParagraph.Id = dbPageParagraph.Id;
            }

            return this.Json(new[] { pageParagraph }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PageParagraphs_Update([DataSourceRequest]DataSourceRequest request, PageParagraphViewModel pageParagraph)
        {
            if (this.ModelState.IsValid)
            {
                var dbPageParagraph = this.pageParagraphServices.Edit(
                    pageParagraph.Id,
                    pageParagraph.PageId,
                    pageParagraph.MainHeading,
                    pageParagraph.MainSubHeading,
                    pageParagraph.Type,
                    pageParagraph.Heading,
                    pageParagraph.Text,
                    pageParagraph.AdditionalHeading,
                    pageParagraph.AdditionalText);
            }

            return this.Json(new[] { pageParagraph }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PageParagraphs_Destroy([DataSourceRequest]DataSourceRequest request, PageParagraphViewModel pageParagraph)
        {
            if (this.ModelState.IsValid)
            {
                this.pageParagraphServices.Delete(pageParagraph.Id);
            }

            return this.Json(new[] { pageParagraph }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
