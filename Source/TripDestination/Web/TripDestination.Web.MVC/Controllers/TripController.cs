﻿namespace TripDestination.Web.MVC.Controllers
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

    public class TripController : BaseController
    {
        public TripController(ITripServices tripServices, ITownProvider townProvider, IStatisticsServices statisticsServices, IDateProvider dateProvider, ITripProvider tripProvider)
        {
            this.TripServices = tripServices;
            this.StatisticsServices = statisticsServices;
            this.TownProvider = townProvider;
            this.DateProvider = dateProvider;
            this.TripProvider = tripProvider;
        }

        public ITripServices TripServices { get; set; }

        public ITownProvider TownProvider { get; set; }

        public IStatisticsServices StatisticsServices { get; set; }

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
        public ActionResult Create(TripInputVIewModel trip)
        {
            if (!this.ModelState.IsValid)
            {
                    return this.View(trip);
            }

            string currentUserId = this.CurrentUser.GetUserId();

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
                    currentUserId
                );

            return this.RedirectToAction("Detailed", "Trip");
        }

        [HttpGet]
        public ActionResult List()
        {
            var day = DateTime.Today;
            var trips = this.TripServices
                .GetForDay(day)
                .To<TripListViewModel>()
                .ToList();

            var weekDays = this.DateProvider
                .GetWeekAhedDays(day)
                .To<WeekDayViewModel>()
                .ToList();

            TripLstViewModel viewModel = new TripLstViewModel()
            {
                Date = day,
                WeekDays = weekDays,
                Trips = trips
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public ActionResult Search(TripLstViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            
            TripSearchInputModel searchModel = model.SearchInputModel;

            return this.View();
        }

        public ActionResult Detailed()
        {
            return this.View();
        }
    }
}