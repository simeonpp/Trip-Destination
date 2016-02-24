namespace TripDestination.Web.MVC.Controllers
{
    using System.Web.Mvc;

    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return this.View("Error");
        }

        public ActionResult NotFound()
        {
            return this.View("Error");
        }

        public ActionResult InternalServer()
        {
            return this.View("Error");
        }
    }
}