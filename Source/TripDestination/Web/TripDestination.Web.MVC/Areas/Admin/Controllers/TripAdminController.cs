﻿namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TripDestination.Data.Models;
    using Services.Data.Contracts;
    using Common.Infrastructure.Mapping;
    using ViewModels;
    using System.Collections.Generic;
    public class TripAdminController : Controller
    {
        private readonly ITripServices tripServices;

        public TripAdminController(ITripServices tripServices)
        {
            this.tripServices = tripServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Trips_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.tripServices
                .GetAll()
                .To<TripAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Trips_Update([DataSourceRequest]DataSourceRequest request, TripAdminViewModel trip)
        {
            if (this.ModelState.IsValid)
            {
                this.tripServices.Edit(
                    trip.Id,
                    trip.DateOfLeaving,
                    trip.AvailableSeats,
                    trip.PlaceOfLeaving,
                    trip.PickUpFromAddress,
                    trip.Description,
                    trip.ETA,
                    new List<string>());
            }

            return this.Json(new[] { trip }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Trips_Destroy([DataSourceRequest]DataSourceRequest request, TripAdminViewModel trip)
        {
            if (this.ModelState.IsValid)
            {
                this.tripServices.AdminDelete(trip.Id);
            }

            return this.Json(new[] { trip }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
