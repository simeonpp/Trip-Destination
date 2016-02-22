namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;
    using System.Linq;
    using ViewModels.Shared;
    using Common.Infrastructure.Constants;
    using Microsoft.AspNet.Identity;

    public class LayoutController : Controller
    {
        public LayoutController(IPageServices pageServices, ITripNotificationServices TripNotificationServices)
        {
            this.PageServices = pageServices;
            this.TripNotificationServices = TripNotificationServices;
        }

        public IPageServices PageServices { get; set; }

        public ITripNotificationServices TripNotificationServices { get; set; }

        [ChildActionOnly]
        //[OutputCache(Duration = CacheTimeConstants.NavigationPartial)]
        public ActionResult NavigationPartial()
        {
            var pages = this.PageServices
                .GetAll()
                .To<PageViewModel>()
                .ToList();

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