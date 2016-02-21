namespace TripDestination.Web.MVC.Controllers
{
    using System;
    using System.Web.Mvc;
    using ViewModels.Trip;
    using Microsoft.AspNet.Identity;
    using Data.Models;
    using System.Linq;
    using ViewModels.Shared;
    using Services.Data.Contracts;
    using Services.Web.Providers.Contracts;
    using TripDestination.Common.Infrastructure.Mapping;
    using Views.Trip;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Script.Serialization;
    using Common.Infrastructure.Constants;
    public class TripController : BaseController
    {
        public const int DefaultItemsPerPage = 9;

        public TripController(ITripServices tripServices, ITownProvider townProvider, IStatisticsServices statisticsServices, IViewServices viewServices, IDateProvider dateProvider, ITripProvider tripProvider)
        {
            this.TripServices = tripServices;
            this.StatisticsServices = statisticsServices;
            this.ViewServices = viewServices;
            this.TownProvider = townProvider;
            this.DateProvider = dateProvider;
            this.TripProvider = tripProvider;
        }

        public ITripServices TripServices { get; set; }

        public ITownProvider TownProvider { get; set; }

        public IStatisticsServices StatisticsServices { get; set; }

        public IViewServices ViewServices { get; set; }

        public IDateProvider DateProvider { get; set; }

        public ITripProvider TripProvider { get; set; }

        [HttpGet]
        [Authorize]
        public ActionResult Create()
        {
            TripInputVIewModel viewModel = new TripInputVIewModel()
            {
                TownsSelectList = this.TownProvider.GetTowns(),
                AvailableSeatsSelectList = this.TripProvider.GetAvailableSeatsSelectList(),
                AddressPickUpSelectList = this.TripProvider.GetAddressPickUpSelectList()
            };

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(TripInputVIewModel trip)
        {
            if (!this.ModelState.IsValid)
            {
                    return this.View(trip);
            }

            string currentUserId = this.User.Identity.GetUserId();

            Trip dbtrip = this.TripServices.Create(
                    trip.FromId,
                    trip.ToId,
                    trip.DateOfLeaving,
                    trip.AvailableSeats,
                    trip.PlaceOfLeaving,
                    trip.PickUpFromAddress,
                    trip.Description,
                    trip.ETA,
                    trip.Price,
                    currentUserId);

            //string slug = string.Format("{0}-{1}", dbtrip.From.Name, dbtrip.To.Name);
            string slug = string.Format("{0}-{1}", "A", "B");
            return this.RedirectToRoute("TripDetails", new { id = dbtrip.Id, slug = slug });
        }

        [HttpGet]
        public ActionResult List(string calendarDate, int page = 1)
        {
            TripLstViewModel viewModel = new TripLstViewModel();
            this.FillRequiredListInformation(viewModel);

            var day = this.DateProvider.CovertDateFromStringToDateTime(calendarDate);

            int allTripsCount = this.TripServices.GetForDay(day).Count();
            int totalPages = (int)Math.Ceiling(allTripsCount / (double)DefaultItemsPerPage);
            int tripsToSkip = (page - 1) * DefaultItemsPerPage;
            var trips = this.TripServices
                .GetForDay(day)
                .Skip(tripsToSkip)
                .Take(DefaultItemsPerPage)
                .To<TripListViewModel>()
                .ToList();

            var weekDays = this.DateProvider
                .GetWeekAhedDays(day)
                .To<WeekDayViewModel>()
                .ToList();

            viewModel.Date = day;
            viewModel.WeekDays = weekDays;
            viewModel.Trips = trips;
            viewModel.TotalPages = totalPages;
            viewModel.CurrentPage = page;
            viewModel.TotalFoundTrips = allTripsCount;

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Search(TripLstViewModel model, int page = 1)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.ItemsPerPage < 0 && model.ItemsPerPage > WebApplicationConstants.MaxItemsPerPage)
            {
                model.ItemsPerPage = DefaultItemsPerPage;
            }

            var dayOfLeaving = model.DateOfLeaving;

            int filteredTripsCount = this.TripServices
                .GetDynamicFIltered(model.FromId, model.ToId, model.Passengers, model.DateOfLeaving, model.DriverName, model.MinPrice, model.MaxPrice, model.OrderBy, model.Sort)
                .Count();
            int totalPages = (int)Math.Ceiling(filteredTripsCount / (double)DefaultItemsPerPage);
            int tripsToSkip = (page - 1) * DefaultItemsPerPage;
            var trips = this.TripServices
                .GetDynamicFIltered(model.FromId, model.ToId, model.Passengers, model.DateOfLeaving, model.DriverName, model.MinPrice, model.MaxPrice, model.OrderBy, model.Sort)
                .Skip(tripsToSkip)
                .Take(DefaultItemsPerPage)
                .To<TripListViewModel>()
                .ToList();

            var weekDays = this.DateProvider
                .GetWeekAhedDays(dayOfLeaving)
                .To<WeekDayViewModel>()
                .ToList();

            model.Date = dayOfLeaving;
            model.WeekDays = weekDays;
            model.Trips = trips;
            model.TotalPages = totalPages;
            model.CurrentPage = page;
            model.TotalFoundTrips = filteredTripsCount;
            this.FillRequiredListInformation(model);

            return this.View("~/Views/Trip/List.cshtml", model);
        }

        private void FillRequiredListInformation(TripLstViewModel viewModel)
        {
            // viewModel.LuggageSpcaceSelectList = this.TripProvider.GetLuggageSpcaceSelectList();
            viewModel.ItemPerPageSelectList = this.TripProvider.GetTripsPerPageSelectList();
            viewModel.TownsSelectList = this.TownProvider.GetTowns();
            viewModel.OrderBySelectListWithSomeNameThatWillNotDoAnyConflictsBecauseMvcIsPlayingAJokeWithAllOfUs = this.TripProvider.GetOrderBySelectList(viewModel.OrderBy);
            viewModel.AvailableSeatsSelectList = this.TripProvider.GetAvailableSeatsSelectList();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var trip = this.TripServices.GetById(id);

            var viewModel = this.Mapper
                .Map<TripDetailedViewModel>(trip);

            var ip = this.Request.ServerVariables["REMOTE_ADDR"];
            this.ViewServices.AddView(trip, ip);

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();
                viewModel.CurrectUserIsDriver = trip.Driver.Id == this.User.Identity.GetUserId();
                viewModel.CurrentUserIsWaitingJoinRequest = this.TripServices.CheckIfUserHasPendingRequest(id, userId);
                viewModel.HasMoreTripComments = this.TripServices.CheckIfTripHasMoreCommentsToLoad(id, WebApplicationConstants.CommentsOfset);
                viewModel.HasMoreUserComments = this.UserServices.CheckIfTripHasMoreCommentsToLoad(trip.DriverId, WebApplicationConstants.CommentsOfset);
                viewModel.CurrentUserLikedTrip = this.TripServices.CheckIfUserLikedTrip(trip, userId);
                viewModel.LikesCount = this.TripServices.GetLikesCount(trip);
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int id)
        {
            var trip = this.TripServices.GetById(id);

            if (trip == null)
            {
                throw new Exception("No such trip.");
            }

            string userId = this.User.Identity.GetUserId();
            if (trip.Driver.Id != userId)
            {
                throw new Exception("Not authorized to edit.");
            }

            var viewModel = this.Mapper.Map<TripEditInputModel>(trip);
            viewModel.AddressPickUpSelectList = this.TripProvider.GetAddressPickUpSelectList();
            viewModel.LeaftAvailabeSeatsSelectList = this.TripProvider.GetleftAvailableSeatsSelectList(trip);
            viewModel.DriverId = userId;

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TripEditInputModel editModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(editModel);
            }

            var trip = this.TripServices.GetById(editModel.Id);

            if (trip.Driver.Id != this.User.Identity.GetUserId())
            {
                throw new Exception("Not authorized to edit.");
            }

            string[] usernamesToBeRemoved = new List<string>().ToArray();
            if (!string.IsNullOrEmpty(editModel.UsernamesToBeRemoved))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                usernamesToBeRemoved = js.Deserialize<string[]>(editModel.UsernamesToBeRemoved);
            }

            var dbTrip = this.TripServices.Edit(
                trip.Id,
                editModel.DateOfLeaving,
                editModel.LeftAvailableSeats,
                editModel.PlaceOfLeaving,
                editModel.PickUpFromAddress,
                editModel.Description,
                editModel.ETA,
                usernamesToBeRemoved);

            return this.RedirectToRoute("TripDetails", new { id = dbTrip.Id, slug = trip.From.Name, area = "" });
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int tripId)
        {
            var userId = this.User.Identity.GetUserId();
            this.TripServices.Delete(tripId, userId);
            return this.RedirectToAction("List");
        }

    }
}