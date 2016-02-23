namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class NewsletterAdminController : Controller
    {
        private readonly INewsletterServices newsletterServices;

        public NewsletterAdminController(INewsletterServices newsletterServices)
        {
            this.newsletterServices = newsletterServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Newsletter_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.newsletterServices
                .GetAll()
                .To<NewsletterAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Newsletter_Destroy([DataSourceRequest]DataSourceRequest request, NewsletterAdminViewModel newsletter)
        {
            if (this.ModelState.IsValid)
            {
                this.newsletterServices.Delete(newsletter.Id);
            }

            return this.Json(new[] { newsletter }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}