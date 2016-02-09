﻿namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using TripDestination.Web.MVC.ViewModels.Shared;

    public class InputVIewModel : BaseTripExtentendedModel
    {
        public IEnumerable<SelectListItem> Towns { get; set; }

        public int TodayCreatedTrips { get; set; }

        public int TodayInProgressTrips { get; set; }

        public int TodayFinishedTrips { get; set; }

        public string TodayTopDestinationTown { get; set; }
    }
}