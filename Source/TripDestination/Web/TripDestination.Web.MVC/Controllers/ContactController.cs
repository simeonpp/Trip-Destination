namespace TripDestination.Web.MVC.Controllers
{
    using Common.Infrastructure.Constants;
    using System.Web.Mvc;
    using ViewModels.Contact;
    public class ContactController : BaseController
    {
        private const string SuccessSendMessage = "Your message was sent successfully";

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.SuccessMessage = this.TempData[WebApplicationConstants.TempDataKeyForContactForm];
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFormViewModel formData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index", formData);
            }

            // TODO: Send/store data
            this.TempData[WebApplicationConstants.TempDataKeyForContactForm] = SuccessSendMessage;

            return this.RedirectToAction("Index");
        }
    }
}