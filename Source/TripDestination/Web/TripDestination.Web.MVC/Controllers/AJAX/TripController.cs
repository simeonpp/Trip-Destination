namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using Services.Data.Contracts;
    using System.Linq;
    using System.Web.Mvc;

    public class TripController : Controller
    {
        private readonly ITripServices tripServices;

        public TripController(ITripServices tripServices)
        {
            this.tripServices = tripServices;
        }

        [Authorize]
        [HttpPost]
        public ActionResult JoinRequest(int tripid)
        {
            
        }
    }
}