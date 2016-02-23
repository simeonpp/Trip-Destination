namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class CookiePolicyController : BaseController
    {
        public ActionResult Index()
        {
            HttpCookie cookiePolicySeen = new HttpCookie("cookiePolicySeen");
            cookiePolicySeen.Value = "true";
            cookiePolicySeen.Expires = DateTime.Now.AddDays(10);
            this.Response.Cookies.Add(cookiePolicySeen);
            return this.View();
        }
    }
}