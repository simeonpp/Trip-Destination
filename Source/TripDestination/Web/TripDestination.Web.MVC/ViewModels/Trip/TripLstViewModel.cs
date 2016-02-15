namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Shared;
    using System.Collections.Generic;
    using Views.Trip;
    public class TripLstViewModel
    {
        public DateTime Date { get; set; }

        public IEnumerable<WeekDayViewModel> WeekDays { get; set; }

        public TripSearchInputModel SearchInputModel { get; set; }

        public IEnumerable<TripListViewModel> Trips { get; set; }
    }
}