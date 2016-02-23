namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class TripCommentAdminController : Controller
    {
        private readonly ITripCommentServices tripCommentServices;

        public TripCommentAdminController(ITripCommentServices tripCommentServices)
        {
            this.tripCommentServices = tripCommentServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult TripComments_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.tripCommentServices
                .GetAll()
                .To<TripCommentAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Trip_Update([DataSourceRequest]DataSourceRequest request, UserCommentsAdminViewModel tripComment)
        {
            if (this.ModelState.IsValid)
            {
                var dbUserComment = this.tripCommentServices.Edit(tripComment.Id, tripComment.Text);
            }

            return this.Json(new[] { tripComment }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Trip_Destroy([DataSourceRequest]DataSourceRequest request, UserCommentsAdminViewModel tripComment)
        {
            if (this.ModelState.IsValid)
            {
                this.tripCommentServices.Delete(tripComment.Id);
            }

            return this.Json(new[] { tripComment }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}