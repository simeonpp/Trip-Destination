namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;
    using System.Linq;
    using ViewModels.Shared;
    using Common.Infrastructure.Constants;
    using Microsoft.AspNet.Identity;
    using Services.Web.Services.Contracts;
    public class LayoutController : Controller
    {
        public LayoutController(IPageServices pageServices, ITripNotificationServices tripNotificationServices, ICacheServices CacheServices)
        {
            this.PageServices = pageServices;
            this.TripNotificationServices = tripNotificationServices;
            this.CacheServices = CacheServices;
        }

        public IPageServices PageServices { get; set; }

        public ITripNotificationServices TripNotificationServices { get; set; }

        public ICacheServices CacheServices { get; set; }

        [ChildActionOnly]
        public ActionResult NavigationPartial()
        {
            var page = this.CacheServices.Get(
                "navigationPages",
                () => this.PageServices
                .GetAll()
                .To<PageViewModel>()
                .ToList(),
                CacheTimeConstants.NavigationPages);

            var currentUserId = this.User.Identity.GetUserId();
            var notifications = this.TripNotificationServices
                .GetNotSeenForUser(currentUserId)
                .To<TripNotificationViewModel>()
                .ToList();

            var viewModel = new NavigationViewModel()
            {
                Pages = pages,
                CurrentUsername = this.User.Identity.GetUserName(),
                Notifications = notifications
            };

            return this.PartialView("~/Views/Shared/_NavigationPartial.cshtml", viewModel);
        }

        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            return this.PartialView("~/Views/Shared/_FooterPartial.cshtml");
        }
    }
}