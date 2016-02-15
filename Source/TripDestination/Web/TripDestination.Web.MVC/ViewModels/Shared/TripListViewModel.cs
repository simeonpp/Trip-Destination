namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System;
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;

    public class TripListViewModel : BaseTripViewModel
    {
        public Town From { get; set; }

        public Town To { get; set; }
    }
}