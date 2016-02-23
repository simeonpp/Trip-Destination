namespace TripDestination.Web.MVC.Controllers
{
    using System.Web;
    using System.Web.Mvc;

    public class CookiePolicyController : BaseController
    {
        public ActionResult Index()
        {
            this.Response.SetCookie(new HttpCookie("cookiePolicySeen", "true"));
            return this.View();
        }
    }
}