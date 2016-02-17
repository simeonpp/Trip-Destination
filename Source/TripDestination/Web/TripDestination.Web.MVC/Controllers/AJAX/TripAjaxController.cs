namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using System;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using Common.Infrastructure.Models;
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

        [Authorize]
        [HttpPost]
        public ActionResult AddComments(int tripId, string commentText)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.AddComment(tripId, userId, commentText);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ApproveJoinRequest(int tripId, string username)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.ApproveJoinRequest(tripId, username, userId);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DisapproveJoinRequest(int tripId, string username)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.DisapproveJoinRequest(tripId, username, userId);
            return this.Json(serviceResponse);
        }

        [HttpGet]
        /// <summary>
        /// Method to load more comments
        /// It used to load comments about trip or users
        /// </summary>
        /// <param name="id">If it is for trip it is int, if for user it is string</param>
        /// <param name="offset">The offset</param>
        /// <param name="type">Possible typesL trip or user</param>
        /// <returns>BaseResponseAjaxModel</returns>
        public ActionResult LoadComments(string id, int offset, string type)
        {
            BaseResponseAjaxModel serviceResponse;

            if (type == "trip")
            {
                int identifier = int.Parse(id);
                serviceResponse = this.tripServices.LoadComments(identifier, offset);
            } else if (type == "user")
            {
                serviceResponse = new BaseResponseAjaxModel();
            } else
            {
                throw new Exception(string.Format("type {0} is not supported", type));
            }

            return this.Json(serviceResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public ActionResult LikeDislikeTrip(int tripId, bool value)
        {
            var userId = this.User.Identity.GetUserId();
            var serviceResponse= this.tripServices.LikeDislike(tripId, userId, value);
            return this.Json(serviceResponse);
        }
    }
}