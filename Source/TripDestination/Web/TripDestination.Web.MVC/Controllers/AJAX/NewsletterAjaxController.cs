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
            var responseData = new BaseResponseAjaxModel();

            if (this.ModelState.IsValid)
            {
                var ip = this.Request.ServerVariables["REMOTE_ADDR"];
                var userAgent = this.Request.UserAgent;

                var dbNewsletter = this.NewsletterServices.Create(model.Email, ip, userAgent);

                responseData.Status = true;
                responseData.Data = dbNewsletter.Email;
            }

            return this.Json(responseData);
        }
    }
}