namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using System;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using Common.Infrastructure.Models;
    using Data.Models;
    using Hubs;

    public class TripAjaxController : Controller
    {
        private readonly ITripServices tripServices;

        private readonly ITripNotificationServices tripNotificaitonServices;

        public TripAjaxController(ITripServices tripServices, ITripNotificationServices tripNotificaitonServices)
        {
            this.tripServices = tripServices;
            this.tripNotificaitonServices = tripNotificaitonServices;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinRequest(int tripId)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.JoinRequest(tripId, userId);
            NotificationHub.UpdateNotify(serviceResponse.SignalRModel);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LeaveTrip(int tripId)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.LeaveTrip(tripId, userId);
            NotificationHub.UpdateNotify(serviceResponse.SignalRModel);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComments(string id, string commentText)
        {
            int tripId = int.Parse(id);
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.AddComment(tripId, userId, commentText);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveJoinRequest(int tripId, string username)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.ApproveJoinRequest(tripId, username, userId);
            NotificationHub.UpdateNotify(serviceResponse.SignalRModel);
            return this.Json(serviceResponse);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisapproveJoinRequest(int tripId, string username)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.DisapproveJoinRequest(tripId, username, userId);
            NotificationHub.UpdateNotify(serviceResponse.SignalRModel);
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
            }
            else
            {
                throw new Exception(string.Format("type {0} is not supported, it must be a trip for this controller", type));
            }

            return this.Json(serviceResponse, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult LikeDislikeTrip(int tripId, bool value)
        {
            var userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.LikeDislike(tripId, userId, value);
            return this.Json(serviceResponse);
        }
    }
}