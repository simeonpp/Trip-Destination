namespace TripDestination.Web.MVC.Controllers.AJAX
{
    using System.Web.Mvc;
    using Services.Data.Contracts;
    using ViewModels.AJAX;
    using Common.Infrastructure.Models;

    public class NewsletterAjaxController : Controller
    {
        public NewsletterAjaxController(INewsletterServices newsletterServices)
        {
            this.NewsletterServices = newsletterServices;
        }

        public INewsletterServices NewsletterServices { get; set; }

        [HttpPost]
        public ActionResult Subscribe(NewsletterInputModel model)
        {
            var ip = this.Request.ServerVariables["REMOTE_ADDR"];
            var userAgent = this.Request.UserAgent;
            var serviceReponse = this.NewsletterServices.Create(model.Email, ip, userAgent);
            return this.Json(serviceReponse);
        }
    }
}