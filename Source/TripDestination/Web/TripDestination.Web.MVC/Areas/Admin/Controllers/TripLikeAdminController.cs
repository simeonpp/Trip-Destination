namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using ViewModels;
    using MVC.Controllers;

    public class TripLikeAdminController : BaseController
    {
        private readonly ILikeServices likeServices;

        public TripLikeAdminController(ILikeServices likeServices)
        {
            this.likeServices = likeServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Likes_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.likeServices
                .GetAll()
                .To<LikeAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Like_Update([DataSourceRequest]DataSourceRequest request, LikeAdminViewModel like)
        {
            if (this.ModelState.IsValid)
            {
                var dbPageParagraph = this.likeServices.Edit(like.Id, like.Value);
            }

            return this.Json(new[] { like }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Like_Destroy([DataSourceRequest]DataSourceRequest request, LikeAdminViewModel like)
        {
            if (this.ModelState.IsValid)
            {
                this.likeServices.Delete(like.Id);
            }

            return this.Json(new[] { like }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}