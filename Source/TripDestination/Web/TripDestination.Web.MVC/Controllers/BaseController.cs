namespace TripDestination.Web.MVC.Controllers
{
    using Data.Models;
    using System.Security.Principal;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        public IIdentity CurrentUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.CurrentUser = this.User.Identity;
        }

    }
}