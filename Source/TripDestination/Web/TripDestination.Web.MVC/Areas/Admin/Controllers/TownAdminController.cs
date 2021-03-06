﻿namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;
    using TripDestination.Web.MVC.Areas.Admin.ViewModels;
    using TripDestination.Web.MVC.Controllers;

    public class TownAdminController : BaseController
    {
        private readonly ITownsServices townServices;

        public TownAdminController(ITownsServices townServices)
        {
            this.townServices = townServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Towns_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.townServices
                .GetAll()
                .To<TownAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Towns_Create([DataSourceRequest]DataSourceRequest request, TownAdminViewModel town)
        {
            if (this.ModelState.IsValid)
            {
                var dbTown = this.townServices.Create(town.Name);
                town.Id = dbTown.Id;
            }

            return this.Json(new[] { town }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Towns_Update([DataSourceRequest]DataSourceRequest request, TownAdminViewModel town)
        {
            if (this.ModelState.IsValid)
            {
                this.townServices.Edit(town.Id, town.Name);
            }

            return this.Json(new[] { town }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Towns_Destroy([DataSourceRequest]DataSourceRequest request, TownAdminViewModel town)
        {
            if (this.ModelState.IsValid)
            {
                this.townServices.Delete(town.Id);
            }

            return this.Json(new[] { town }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
