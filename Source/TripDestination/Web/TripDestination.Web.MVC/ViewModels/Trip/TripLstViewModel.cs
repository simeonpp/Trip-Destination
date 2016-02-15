namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Shared;
    using System.Collections.Generic;
    using Views.Trip;
    using System.Web.Mvc;
    public class TripLstViewModel
    {
        public DateTime Date { get; set; }

        public IEnumerable<WeekDayViewModel> WeekDays { get; set; }

        public TripSearchInputModel SearchInputModel { get; set; }

        public IEnumerable<SelectListItem> TownsSelectList { get; set; }

        public IEnumerable<SelectListItem> LuggageSpcaceSelectList { get; set; }

        public IEnumerable<SelectListItem> ItemPerPageSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList { get; set; }

        public IEnumerable<TripListViewModel> Trips { get; set; }
    }
}