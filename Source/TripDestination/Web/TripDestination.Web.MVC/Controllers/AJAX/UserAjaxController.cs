namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using Common.Infrastructure.Models;
    using Microsoft.AspNet.Identity;
    using Services.Data.Contracts;
    using System;
    using System.Web.Mvc;

    public class UserAjaxController : Controller
    {
        private readonly IUserServices userServices;

        public UserAjaxController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComments(string id, string commentText)
        {
            string currentUserId = this.User.Identity.GetUserId();
            var serviceResponse = this.userServices.AddComment(id, currentUserId, commentText);
            return this.Json(serviceResponse);
        }

        public ActionResult LoadComments(string id, int offset, string type)
        {
            BaseResponseAjaxModel serviceResponse;

            if (type == "user")
            {
                serviceResponse = this.userServices.LoadComments(id, offset);
            }
            else
            {
                throw new Exception(string.Format("type {0} is not supported, it must be a user for this controller", type));
            }

            return this.Json(serviceResponse, JsonRequestBehavior.AllowGet);
        }
    }
}