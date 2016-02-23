namespace TripDestination.Web.MVC.Controllers
{
    using AutoMapper;
    using Data.Models;
    using System.Security.Principal;
    using System.Web.Mvc;
    using Common.Infrastructure.Mapping;
    using Services.Web.Services.Contracts;
    using Services.Data.Contracts;
    using Microsoft.AspNet.Identity;
    public abstract class BaseController : Controller
    {
        // Need to be public, otherwise Autofac will no be able to autowire it
        public IUserServices UserServices { get; set; }

        // Need to be public, otherwise Autofac will no be able to autowire it
        public ICacheServices Cache { get; set; }

        protected User CurrentUser { get; set; }

        protected IMapper Mapper { get; private set; }

        public MapperConfiguration MapperConfiguration { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.CurrentUser = this.UserServices.GetByUsername(this.User.Identity.GetUserName());
            }

            this.MapperConfiguration = AutoMapperConfig.Configuration;
            this.Mapper = AutoMapperConfig.Mapper;

            this.ViewData["cookiePolicySeen"] = this.Request.Cookies.Get("cookiePolicySeen") == null ? false : true;

            base.OnActionExecuting(filterContext);
        }
    }
}