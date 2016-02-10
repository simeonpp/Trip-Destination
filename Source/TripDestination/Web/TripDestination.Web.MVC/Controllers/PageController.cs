namespace TripDestination.Web.MVC.Controllers
{
    using Ninject;
    using Services.Contracts;
    using System.Web.Mvc;
    using Data.Models;

    public class PageController : BaseController
    {
        [Inject]
        public IPageServices PageServices { get; set; }

        public ActionResult Index(int id)
        {
            Page page = this.PageServices.GetById(id);

            if (page == null)
            {
                return this.View("Error");
            }

            this.ViewBag.Id = page.Heading;
            return this.View();
        }
    }
}