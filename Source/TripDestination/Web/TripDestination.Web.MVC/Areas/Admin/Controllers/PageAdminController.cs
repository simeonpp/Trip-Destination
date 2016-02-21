namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Web.MVC.Areas.Admin.ViewModels;
    using TripDestination.Common.Infrastructure.Mapping;

    public class PageAdminController : Controller
    {
        private IPageServices pageServices;

        public PageAdminController(IPageServices pageServices)
        {
            this.pageServices = pageServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Pages_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.pageServices
                .GetAll()
                .To<PageAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pages_Create([DataSourceRequest]DataSourceRequest request, PageAdminViewModel page)
        {
            if (this.ModelState.IsValid)
            {
                var dbPage = this.pageServices.Create(page.Heading, page.SubHeading, page.Layout);
                page.Id = dbPage.Id;
            }

            return this.Json(new[] { page }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pages_Update([DataSourceRequest]DataSourceRequest request, PageAdminViewModel page)
        {
            if (this.ModelState.IsValid)
            {
                this.pageServices.Edit(page.Id, page.Heading, page.SubHeading, page.Layout);
            }

            return this.Json(new[] { page }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Pages_Destroy([DataSourceRequest]DataSourceRequest request, PageAdminViewModel page)
        {
            if (this.ModelState.IsValid)
            {
                this.pageServices.Delete(page.Id);
            }

            return this.Json(new[] { page }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
