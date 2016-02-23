namespace TripDestination.Web.MVC.ViewModels.Shared
{
    using System;
    using AutoMapper;
    using Common.Infrastructure.Mapping;
    using TripDestination.Data.Models;
    using Trip;

    public class TripListViewModel : BaseTripViewModel, IMapFrom<Trip>
    {
        public TownViewModel From { get; set; }

        public TownViewModel To { get; set; }
    }
}