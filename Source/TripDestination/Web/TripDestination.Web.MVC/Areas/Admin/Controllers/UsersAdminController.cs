namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using MVC.Controllers;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels;

    public class UsersAdminController : BaseController
    {
        private readonly IUserServices userServices;

        public UsersAdminController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.userServices
                .GetAll()
                .To<UsersAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_CreateAdmin([DataSourceRequest]DataSourceRequest request, UsersAdminViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var dbUser = this.userServices.CreateAdmin(user.Username, user.Email, user.Password, user.FirstName, user.LastName);
                user.Id = dbUser.Id;
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Update([DataSourceRequest]DataSourceRequest request, UsersAdminViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                var dbUser = this.userServices.Edit(user.Id, user.Email, user.FirstName, user.LastName);
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, UsersAdminViewModel user)
        {
            if (this.ModelState.IsValid)
            {
                this.userServices.Delete(user.Id);
            }

            return this.Json(new[] { user }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}