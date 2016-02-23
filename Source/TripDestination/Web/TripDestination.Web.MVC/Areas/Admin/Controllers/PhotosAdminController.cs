namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MVC.Controllers;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class PhotosAdminController : BaseController
    {
        private readonly IPhotoServices photoServices;

        public PhotosAdminController(IPhotoServices photoServices)
        {
            this.photoServices = photoServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Photo_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.photoServices
                .GetAll()
                .To<PhotosAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}