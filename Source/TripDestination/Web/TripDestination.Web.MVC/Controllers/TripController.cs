namespace TripDestination.Web.MVC.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels.Trip;
    using Microsoft.AspNet.Identity;
    using Services.Contracts;
    using Ninject;
    using Data.Models;

    public class TripController : BaseController
    {
        [Inject]
        public ITripServices TripServices { get; set; }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            var towns = this.GetTowns();
            InputVIewModel model = new InputVIewModel()
            {
                Towns = towns
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