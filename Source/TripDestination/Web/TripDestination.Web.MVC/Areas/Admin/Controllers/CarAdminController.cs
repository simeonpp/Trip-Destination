namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Web.Mvc;
    using Common.Infrastructure.Mapping;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using Services.Data.Contracts;
    using TripDestination.Data.Models;
    using ViewModels;
    using MVC.Controllers;

    public class CarAdminController : BaseController
    {
        private readonly ICarServices carServices;

        public CarAdminController(ICarServices carServices)
        {
            this.carServices = carServices;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Cars_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = this.carServices
                .GetAll()
                .To<CarAdminViewModel>()
                .ToDataSourceResult(request);

            return this.Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cars_Update([DataSourceRequest]DataSourceRequest request, CarAdminViewModel car)
        {
            if (this.ModelState.IsValid)
            {
                this.carServices.Update(
                    car.Id,
                    car.Brand,
                    car.CarModel,
                    car.Color,
                    car.Year,
                    car.TotalSeats,
                    car.SpaceForLugage,
                    car.Description);
            }

            return this.Json(new[] { car }.ToDataSourceResult(request, this.ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cars_Destroy([DataSourceRequest]DataSourceRequest request, CarAdminViewModel car)
        {
            if (this.ModelState.IsValid)
            {
                this.carServices.Delete(car.Id);
            }

            return this.Json(new[] { car }.ToDataSourceResult(request, this.ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
