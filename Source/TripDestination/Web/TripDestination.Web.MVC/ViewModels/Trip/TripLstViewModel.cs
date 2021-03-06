﻿namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Shared;
    using System.Collections.Generic;
    using Views.Trip;
    using System.Web.Mvc;

    public class TripLstViewModel : TripSearchInputModel
    {
        public TripLstViewModel()
        {
            this.Sort = "ASC";
        }

        public DateTime Date { get; set; }

        public string FormattedMonthForCalendar
        {
            get
            {
                return this.Date.ToString("MMMM").Substring(0, 3);
            }
        }

        public string Sort { get; set; }

        public string OrderBy { get; set; }

        public IEnumerable<WeekDayViewModel> WeekDays { get; set; }

        public IEnumerable<SelectListItem> TownsSelectList { get; set; }

        public IEnumerable<SelectListItem> LuggageSpcaceSelectList { get; set; }

        public IEnumerable<SelectListItem> ItemPerPageSelectList { get; set; }

        public IEnumerable<SelectListItem> AvailableSeatsSelectList { get; set; }

        public IEnumerable<SelectListItem> OrderBySelectListWithSomeNameThatWillNotDoAnyConflictsBecauseMvcIsPlayingAJokeWithAllOfUs { get; set; }

        public IEnumerable<TripListViewModel> Trips { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalFoundTrips { get; set; }

        public string SelectedDateAsString { get; set; }
    }
}