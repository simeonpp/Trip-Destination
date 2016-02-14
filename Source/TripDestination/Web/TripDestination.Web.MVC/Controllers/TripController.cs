namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.Web.Mvc;
    using ViewModels.Trip;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Shared;
    using Services.Data.Contracts;

    public class TripController : BaseController
    {
        public TripController(ITripServices tripServices, ITownsServices townServices, IStatisticsServices statisticsServices)
        {
            this.TripServices = tripServices;
            this.TownServices = townServices;
            this.StatisticsServices = statisticsServices;
        }

        public ITripServices TripServices { get; set; }

        public ITownsServices TownServices { get; set; }

        public IStatisticsServices StatisticsServices { get; set; }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var towns = this.TownServices
                    .GetAll()
                    .Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    })
                    .ToList();

            TripInputVIewModel viewModel = new TripInputVIewModel()
            {
                Towns = towns
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TripInputVIewModel trip)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(trip);
            }

            string currentUserId = this.CurrentUser.GetUserId();

            Trip dbtrip = this.TripServices.Create(
                    trip.FromId,
                    trip.ToId,
                    trip.DateOfLeaving,
                    trip.AvailableSeats,
                    trip.PlaceOfLeaving,
                    trip.PickUpFromAddress,
                    trip.Description,
                    trip.ETA,
                    trip.Price,
                    currentUserId
                );

            return this.RedirectToAction("Detailed", "Trip");
        }

        [HttpGet]
        public ActionResult List()
        {
            var day = DateTime.Today;
            var trips = this.TripServices
                .GetForDay(day)
                .ProjectTo<TripListViewModel>(this.MapperConfiguration)
                .ToList();

            TripLstViewModel viewModel = new TripLstViewModel()
            {
                Date = day,
                Trips = trips
            };

            return this.View(viewModel);
        }

        public ActionResult Detailed()
        {
            return this.View();
        }
    }
}