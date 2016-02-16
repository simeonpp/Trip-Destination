namespace TripDestination.Web.MVC.Controllers.AJAX
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
        public ActionResult JoinRequest(int tripid)
        {
            string userId = this.User.Identity.GetUserId();
            var serviceResponse = this.tripServices.JoinRequest(tripid, userId);
            return this.Json(serviceResponse);
        }
    }
}