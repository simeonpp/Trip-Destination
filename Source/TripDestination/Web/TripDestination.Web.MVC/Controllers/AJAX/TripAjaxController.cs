﻿namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    public class TripAjaxController : Controller
    {
        private readonly ITripServices tripServices;

        public TripAjaxController(ITripServices tripServices)
        {
            this.tripServices = tripServices;
        }

        [Authorize]
        [HttpPost]
        public ActionResult JoinRequest(int tripId)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.JoinRequest(tripId, userId);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        public ActionResult LeaveTrip(int tripId)
        {
            string userId = this.User.Identity.GetUserId(); ;
            var serviceResponse = this.tripServices.LeaveTrip(tripId, userId);
            return this.Json(serviceResponse);
        }


    }
}