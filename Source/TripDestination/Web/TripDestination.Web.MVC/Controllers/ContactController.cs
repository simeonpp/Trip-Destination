namespace TripDestination.Web.MVC.Controllers
{
    using System.Web.Mvc;

    public class ContactController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult ContactForm()
        {
            return this.RedirectToAction("Index");
        }
    }
}