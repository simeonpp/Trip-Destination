namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MVC.Controllers;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class UserCommentAdminController : BaseController
    {
        private readonly IUserCommentServices userCommentServices;

        public UserCommentAdminController(IUserCommentServices userCommentServices)
        {
            this.userCommentServices = userCommentServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult UserComments_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.userCommentServices
                .GetAll()
                .To<UserCommentsAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserComments_Update([DataSourceRequest]DataSourceRequest request, UserCommentsAdminViewModel userComment)
        {
            if (this.ModelState.IsValid)
            {
                var dbUserComment = this.userCommentServices.Edit(userComment.Id, userComment.Text);
            }

            return this.Json(new[] { userComment }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UserComments_Destroy([DataSourceRequest]DataSourceRequest request, UserCommentsAdminViewModel userComment)
        {
            if (this.ModelState.IsValid)
            {
                this.userCommentServices.Delete(userComment.Id);
            }

            return this.Json(new[] { userComment }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}