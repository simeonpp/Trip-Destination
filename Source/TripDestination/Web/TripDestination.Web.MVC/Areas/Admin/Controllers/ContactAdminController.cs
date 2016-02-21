namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class ContactAdminController : Controller
    {
        private readonly IContactFormServices contactFormServices;

        public ContactAdminController(IContactFormServices contactFormServices)
        {
            this.contactFormServices = contactFormServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Contact_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.contactFormServices
                .GetAll()
                .To<ContactFormAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Contact_Destroy([DataSourceRequest]DataSourceRequest request, ContactFormAdminViewModel trip)
        {
            if (this.ModelState.IsValid)
            {
                this.contactFormServices.Delete(trip.Id);
            }

            return this.Json(new[] { trip }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}