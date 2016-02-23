namespace TripDestination.Web.MVC.Controllers
{
    using Common.Infrastructure.Constants;
    using Data.Models;
    using Services.Data.Contracts;
    using System.Web.Mvc;
    using ViewModels.Contact;

    public class ContactController : BaseController
    {
        private const string SuccessSendMessage = "Your message was sent successfully";

        public ContactController(IContactFormServices contactFormServices)
        {
            this.ContactFormServices = contactFormServices;
        }

        public IContactFormServices ContactFormServices { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            this.ViewBag.SuccessMessage = this.TempData[WebApplicationConstants.TempDataKeyForContactForm];
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactFormInputModel formData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Index", formData);
            }

            var ip = this.Request.ServerVariables["REMOTE_ADDR"];
            ContactForm contactForm = this.ContactFormServices.Create(formData.Name, formData.Email, formData.Subject, formData.Message, ip);
            this.TempData[WebApplicationConstants.TempDataKeyForContactForm] = SuccessSendMessage;

            return this.RedirectToAction("Index");
        }
    }
}