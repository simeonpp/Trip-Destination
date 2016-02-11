namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Ninject;
    using AutoMapper.QueryableExtensions;
    using TripDestination.Common.Infrastructure.Constants;
    using TripDestination.Services.Contracts;
    using TripDestination.Web.MVC.ViewModels.Home;
    using ViewModels.Shared;

    public class HomeController : BaseController
    {
        [Inject]
        public ITripServices TripServices { get; set; }

        public ActionResult Index()
        {
            //var topDesinations = this.GetTopDestinations();

            //var todayTrips = this.TripServices.GetTodayTrips(WebApplicationConstants.HomepageTripsPerSection)
            //    .ProjectTo<TripListViewModel>(this.MapperConfiguration)
            //    .ToList();

            //var latestTrips = this.TripServices.GetLatest(WebApplicationConstants.HomepageTripsPerSection)
            //    .ProjectTo<TripListViewModel>(this.MapperConfiguration)
            //    .ToList();

            //HomepageViewModel viewModel = new HomepageViewModel()
            //{
            //    TopDestinations = topDesinations,
            //    TodayTrips = todayTrips,
            //    LatestTrips = latestTrips
            //};

            //return View(viewModel);
            return View();
        }

        private IEnumerable<Tuple<string, string>> GetTopDestinations()
        {
            var topTownsFrom = this.TripServices.GetTopTownsDestination();
            // Getting 3 towns names will avoid duplicates even in worst case - FROM[X1, X2], TO[X1, X2, X3]
            var topTownsTo = this.TripServices.GetTopTownsDestination(false, 3);

            var tupleTownsList = new List<Tuple<string, string>>();
            int currentCounter = 0;
            foreach (var fromTown in topTownsFrom)
            {
                foreach (var toTown in topTownsTo)
                {
                    if (currentCounter >= 2)
                    {
                        break;
                    }

                    if (toTown == fromTown)
                    {
                        continue;
                    }

                    tupleTownsList.Add(new Tuple<string, string>(fromTown, toTown));
                    currentCounter++;
                }

                currentCounter = 0;
            }

            return tupleTownsList;
        }
        
    }
}