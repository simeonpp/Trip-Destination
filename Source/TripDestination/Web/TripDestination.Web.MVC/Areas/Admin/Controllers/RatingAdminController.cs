namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using System.Linq;
    using MVC.Controllers;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class RatingAdminController : BaseController
    {
        private readonly IRatingServices ratingServices;

        public RatingAdminController(IRatingServices ratingServices)
        {
            this.ratingServices = ratingServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Ratings_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.ratingServices
                .GetAll()
                .To<RatingAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Rating_Update([DataSourceRequest]DataSourceRequest request, RatingAdminViewModel rating)
        {
            if (this.ModelState.IsValid)
            {
                var dbRating = this.ratingServices.Edit(rating.Id, (int)rating.Value);
            }

            return this.Json(new[] { rating }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Rating_Destroy([DataSourceRequest]DataSourceRequest request, RatingAdminViewModel rating)
        {
            if (this.ModelState.IsValid)
            {
                this.ratingServices.Delete(rating.Id);
            }

            return this.Json(new[] { rating }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}