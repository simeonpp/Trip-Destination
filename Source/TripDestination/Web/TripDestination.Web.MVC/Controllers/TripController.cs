namespace TripDestination.Web.MVC.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels.Trip;
    using Microsoft.AspNet.Identity;
    using Services.Contracts;
    using Ninject;
    using Data.Models;
    using System.Linq;

    public class TripController : BaseController
    {
        [Inject]
        public ITripServices TripServices { get; set; }
            
        [Inject]
        public ITownsServices TownServices { get; set; }

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

            InputVIewModel model = new InputVIewModel()
            {
                Towns = towns,
                TodayCreatedTrips = this.TripServices.GetTodayCreatedCount(),
                TodayInProgressTrips = this.TripServices.GetTodayInProgressCount(),
                TodayFinishedTrips = this.TripServices.GetTodayFinishedCount(),
                TodayTopDestinationTown = this.TripServices.GetTodayTopDestination()
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InputVIewModel trip)
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

        public ActionResult Detailed()
        {
            return this.View();
        }
    }
}