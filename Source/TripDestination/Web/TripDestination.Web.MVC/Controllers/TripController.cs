namespace TripDestination.Web.MVC.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using ViewModels.Trip;
    using Microsoft.AspNet.Identity;
    using Services.Contracts;
    using Ninject;
    using Data.Models;
    using AutoMapper;
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
            
            string userId = this.CurrentUser.GetUserId();

            Trip dbTripToBeSaved;

            //Mapper.CreateMap<>
            


            Mapper.Map(trip, dbTripToBeSaved);

            int a = this.TripServices.GetMe();

            //TODO: call service to save the trip in the Db
            return this.RedirectToAction("List", "Trip");
        }

        private IEnumerable<SelectListItem> GetTowns()
        {
            var towns = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Sofia", Value = "1" },
                new SelectListItem() { Text = "Tervel", Value = "12" },
                new SelectListItem() { Text = "Ruse", Value = "15" },
                new SelectListItem() { Text = "Septemvri", Value = "25" },
            };

            return towns;
        }
    }
}