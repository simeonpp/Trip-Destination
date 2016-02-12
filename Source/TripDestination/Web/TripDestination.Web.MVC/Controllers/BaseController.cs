namespace TripDestination.Web.MVC.Controllers
{
    using AutoMapper;
    using Data.Models;
    using System.Security.Principal;
    using System.Web.Mvc;
    using Common.Infrastructure.Mapping;
    using Services.Web.Services.Contracts;
    public abstract class BaseController : Controller
    {
        // Need to be public, otherwise Autofac will no be able to autowire it
        public ICacheServices Cache { get; set; }

        protected IIdentity CurrentUser { get; set; }

        protected IMapper Mapper { get; private set; }

        protected MapperConfiguration MapperConfiguration { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            this.CurrentUser = this.User.Identity;
            this.MapperConfiguration = AutoMapperConfig.Configuration;
            this.Mapper = AutoMapperConfig.Mapper;
        }

    }
}