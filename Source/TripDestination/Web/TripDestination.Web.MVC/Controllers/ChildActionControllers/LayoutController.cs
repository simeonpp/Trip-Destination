﻿namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using TripDestination.Common.Infrastructure.Mapping;
    using System.Linq;
    using ViewModels.Shared;
    public class LayoutController : Controller
    {
        public LayoutController(IPageServices pageServices)
        {
            this.PageServices = pageServices;
        }

        public IPageServices PageServices { get; set; }

        [ChildActionOnly]
        public ActionResult NavigationPartial()
        {
            var pages = this.PageServices
                .GetAll()
                .To<PageViewModel>()
                .ToList();

            var viewModel = new NavigationViewModel()
            {
                Pages = pages
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