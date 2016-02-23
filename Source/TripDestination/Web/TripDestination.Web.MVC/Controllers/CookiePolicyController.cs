namespace TripDestination.Web.MVC.Controllers
{
    using Common.Infrastructure.Constants;
    using System;
    using System.Web;
    using System.Web.Mvc;

    public class CookiePolicyController : BaseController
    {
        public ActionResult Index()
        {
            HttpCookie cookiePolicy = new HttpCookie(WebApplicationConstants.CookiePolicyCookieKey);
            cookiePolicy[WebApplicationConstants.CookiePolicyCookieKey] = "true";
            cookiePolicy.Expires = DateTime.Now.AddDays(5);
            this.HttpContext.Response.Cookies.Add(cookiePolicy);
            return this.View();
        }
    }
}