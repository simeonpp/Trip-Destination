namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class ViewAdminController : Controller
    {
        private readonly IViewServices viewServices;

        public ViewAdminController(IViewServices viewServices)
        {
            this.viewServices = viewServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult View_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.viewServices
                .GetAll()
                .To<ViewAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Views_Destroy([DataSourceRequest]DataSourceRequest request, ViewAdminViewModel view)
        {
            if (this.ModelState.IsValid)
            {
                this.viewServices.Delete(view.Id);
            }

            return this.Json(new[] { view }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}