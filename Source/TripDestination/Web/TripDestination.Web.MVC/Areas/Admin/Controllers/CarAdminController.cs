namespace TripDestination.Web.MVC.Areas.Admin.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;
    using TripDestination.Data.Models;
    using TripDestination.Data.Data;

    public class CarAdminController : Controller
    {
        private TripDestinationDbContext db = new TripDestinationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cars_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Car> cars = db.Cars;
            DataSourceResult result = cars.ToDataSourceResult(request, c => new CarAdminViewModel 
            {
                TotalSeats = c.TotalSeats,
                Brand = c.Brand,
                Model = c.Model,
                Color = c.Color,
                Year = c.Year,
                SpaceForLugage = c.SpaceForLugage,
                Description = c.Description
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cars_Create([DataSourceRequest]DataSourceRequest request, CarAdminViewModel car)
        {
            if (ModelState.IsValid)
            {
                var entity = new Car
                {
                    TotalSeats = car.TotalSeats,
                    Brand = car.Brand,
                    Model = car.Model,
                    Color = car.Color,
                    Year = car.Year,
                    SpaceForLugage = car.SpaceForLugage,
                    Description = car.Description
                };

                db.Cars.Add(entity);
                db.SaveChanges();
                car.Id = entity.Id;
            }

            return Json(new[] { car }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cars_Update([DataSourceRequest]DataSourceRequest request, CarAdminViewModel car)
        {
            if (ModelState.IsValid)
            {
                var entity = new Car
                {
                    Id = car.Id,
                    TotalSeats = car.TotalSeats,
                    Brand = car.Brand,
                    Model = car.Model,
                    Color = car.Color,
                    Year = car.Year,
                    SpaceForLugage = car.SpaceForLugage,
                    Description = car.Description
                };

                db.Cars.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { car }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Cars_Destroy([DataSourceRequest]DataSourceRequest request, CarAdminViewModel car)
        {
            if (ModelState.IsValid)
            {
                var entity = new Car
                {
                    Id = car.Id,
                    TotalSeats = car.TotalSeats,
                    Brand = car.Brand,
                    Model = car.Model,
                    Color = car.Color,
                    Year = car.Year,
                    SpaceForLugage = car.SpaceForLugage,
                    Description = car.Description
                };

                db.Cars.Attach(entity);
                db.Cars.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { car }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
