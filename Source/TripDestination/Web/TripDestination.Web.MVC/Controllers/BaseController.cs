namespace TripDestination.Web.MVC.Controllers
{
    using AutoMapper;
    using Data.Models;
    using System.Security.Principal;
    using System.Web.Mvc;

    public abstract class BaseController : Controller
    {
        protected IIdentity CurrentUser { get; set; }

        protected MapperConfiguration MapperConfiguration { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.CurrentUser = this.User.Identity;
            this.MapperConfiguration = AutoMapperConfig.Configuration;
        }

    }
}