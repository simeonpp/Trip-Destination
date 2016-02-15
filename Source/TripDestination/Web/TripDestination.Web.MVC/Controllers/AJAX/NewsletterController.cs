namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using System.Web.Mvc;
    using ViewModels.AJAX;

    public class NewsletterController : Controller
    {
        public ActionResult Subscribe(NewsletterInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var ip = this.Request.ServerVariables["REMOTE_ADDR"];
                var userAgent = this.Request.UserAgent;
            }

            return null;
        }
    }
}