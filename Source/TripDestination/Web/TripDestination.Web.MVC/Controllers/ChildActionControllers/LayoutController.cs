namespace TripDestination.Web.MVC.Controllers.ChildActionControllers
{
    using System.Web.Mvc;

    public class LayoutController : Controller
    {
        [ChildActionOnly]
        public ActionResult FooterPartial()
        {
            return this.PartialView("~/Views/Shared/_FooterPartial.cshtml");
        }
    }
}