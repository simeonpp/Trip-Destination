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
    using Hubs;
    using Common.Infrastructure.Models;

    public class TripController : BaseController
    {
        public const int DefaultItemsPerPage = 9;

        public TripController(ITripServices tripServices, IRatingServices ratingServices, ITownProvider townProvider, IStatisticsServices statisticsServices, IViewServices viewServices, IDateProvider dateProvider, ITripProvider tripProvider, INotificationServices notificationServices)
        {
            this.TripServices = tripServices;
            this.RatingServices = ratingServices;
            this.StatisticsServices = statisticsServices;
            this.ViewServices = viewServices;
            this.TownProvider = townProvider;
            this.DateProvider = dateProvider;
            this.TripProvider = tripProvider;
            this.NotificationServices = notificationServices;
        }

        public ITripServices TripServices { get; set; }

        public IRatingServices RatingServices { get; set; }

        public ITripNotificationServices TripNotificationServices { get; set; }

        public ITownProvider TownProvider { get; set; }

        public IStatisticsServices StatisticsServices { get; set; }

        public IViewServices ViewServices { get; set; }

        public IDateProvider DateProvider { get; set; }

        public ITripProvider TripProvider { get; set; }

        public INotificationServices NotificationServices { get; set; }

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
            if (trip.FromId == trip.ToId)
            {
                this.ModelState.AddModelError("From", "From and To towns should be different");
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData[WebApplicationConstants.TempDataMessageKey] = "From and To towns should be different";
                return this.RedirectToAction("Create");
            }

            string currentUserId = this.User.Identity.GetUserId();

            Trip serviceResponceTrip = this.TripServices.Create(
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

            this.NotificationServices.Create(
                serviceResponceTrip.Id,
                currentUserId,
                currentUserId,
                NotificationConstants.CloseTripDriverRequestTitle,
                string.Format(NotificationConstants.CloseTripDriverRequestFormat, serviceResponceTrip.FromId, serviceResponceTrip.ToId, serviceResponceTrip.DateOfLeaving.ToString("dd/MM/yyyy HH:mm")),
                NotificationKey.FinishTripDriverRequest,
                serviceResponceTrip.DateOfLeaving.AddDays(NotificationConstants.CloseTripDriverRequestAvaialableDaysAfterTripFinished),
                serviceResponceTrip.ETA);

            return this.RedirectToRoute("TripDetails", new { id = serviceResponceTrip.Id });
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
            int totalPages = (int)Math.Ceiling(filteredTripsCount / (double)model.ItemsPerPage);
            int tripsToSkip = (page - 1) * model.ItemsPerPage;
            var trips = this.TripServices
                .GetDynamicFIltered(model.FromId, model.ToId, model.Passengers, model.DateOfLeaving, model.DriverName, model.MinPrice, model.MaxPrice, model.OrderBy, model.Sort)
                .Skip(tripsToSkip)
                .Take(model.ItemsPerPage)
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
            model.SelectedDateAsString = model.DateOfLeaving.ToString("yyyy-MM-dd");
            this.FillRequiredListInformation(model);

            return this.View("~/Views/Trip/List.cshtml", model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var trip = this.TripServices.GetByIdWithStatusCheck(id);

            var viewModel = this.Mapper
                .Map<TripDetailedViewModel>(trip);

            var ip = this.Request.ServerVariables["REMOTE_ADDR"];
            this.ViewServices.AddView(trip, ip);

            if (this.User.Identity.IsAuthenticated)
            {
                var userId = this.User.Identity.GetUserId();
                viewModel.CurrectUserIsDriver = trip.Driver.Id == this.User.Identity.GetUserId();
                viewModel.CurrentUserIsWaitingJoinRequest = this.TripServices.CheckIfUserHasPendingRequest(id, userId);
                viewModel.CurrentUserIsJoinedTrip = this.TripServices.CheckIfUserIsJoinedTrip(id, userId);
                viewModel.HasMoreTripComments = this.TripServices.CheckIfTripHasMoreCommentsToLoad(id, WebApplicationConstants.CommentsOfset);
                viewModel.HasMoreUserComments = this.UserServices.CheckIfTripHasMoreCommentsToLoad(trip.DriverId, WebApplicationConstants.CommentsOfset);
                viewModel.CurrentUserLikedTrip = this.TripServices.CheckIfUserLikedTrip(trip, userId);
                viewModel.LikesCount = this.TripServices.GetLikesCount(trip);
            }

            viewModel.DriverComments = this.UserServices.GetComments(trip.DriverId).To<BaseCommentViewModel>().ToList();
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

            var serviceResponse = this.TripServices.Edit(
                trip.Id,
                editModel.DateOfLeaving,
                editModel.LeftAvailableSeats,
                editModel.PlaceOfLeaving,
                editModel.PickUpFromAddress,
                editModel.Description,
                editModel.ETA,
                usernamesToBeRemoved);

            NotificationHub.UpdateNotify(serviceResponse.SignalRModel);
            return this.RedirectToRoute("TripDetails", new { id = trip.Id, slug = trip.From.Name, area = string.Empty });
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

        [Authorize]
        [HttpGet]
        public ActionResult Rate(int id)
        {
            string userId = this.User.Identity.GetUserId();
            Trip trip = this.TripServices.GetById(id);
            bool userCanRate = this.TripProvider.UserCanRateTrip(trip, userId);

            if (!userCanRate)
            {
                return this.RedirectToRoute("/");
            }

            var viewModel = new TripRateInputModel()
            {
                TripId = id,
                CurrentUserIsDriver = trip.DriverId == userId
            };

            var passengers = trip.Passengers.Select(p => p.User);
            var mapperdPassnegers = passengers.AsQueryable().To<BaseUserViewModel>().ToList();
            viewModel.Passengers = mapperdPassnegers;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(TripRateInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            string userId = this.User.Identity.GetUserId();
            Trip trip = this.TripServices.GetById(model.TripId);
            bool userCanRate = this.TripProvider.UserCanRateTrip(trip, userId);

            if (!userCanRate)
            {
                return this.RedirectToRoute("/");
            }

            TripNotification tripNotification = this.Trip

            if (trip.DriverId == userId)
            {
                var passengers = trip.Passengers.Select(p => p.UserId).ToList(); ;
                for (int i = 0; i < passengers.Count; i++)
                {
                    string passengerUsername = passengers.ElementAt(i);
                    int passengerRating = this.TripProvider.GetValidRate(model.PassengerRatings.ElementAt(i));
                    this.RatingServices.RateUser(passengerUsername, userId, passengerRating);
                }

                this.TempData[WebApplicationConstants.TempDataMessageKey] = "You have succesfully rate your passengers.";
            }
            else
            {
                int driverRating = this.TripProvider.GetValidRate(model.DriverRating);
                this.RatingServices.RateUser(trip.DriverId, userId, driverRating);
                this.TempData[WebApplicationConstants.TempDataMessageKey] = "You have succesfully rate this driver.";
            }

            return this.RedirectToRoute("TripDetails", new { id = model.TripId, slug = string.Format("{0}-{1}", trip.From.Name, trip.To.Name) });
        }

        private void FillRequiredListInformation(TripLstViewModel viewModel)
        {
            // viewModel.LuggageSpcaceSelectList = this.TripProvider.GetLuggageSpcaceSelectList();
            viewModel.ItemPerPageSelectList = this.TripProvider.GetTripsPerPageSelectList();
            viewModel.TownsSelectList = this.TownProvider.GetTowns();
            viewModel.OrderBySelectListWithSomeNameThatWillNotDoAnyConflictsBecauseMvcIsPlayingAJokeWithAllOfUs = this.TripProvider.GetOrderBySelectList(viewModel.OrderBy);
            viewModel.AvailableSeatsSelectList = this.TripProvider.GetAvailableSeatsSelectList();
        }
    }
}