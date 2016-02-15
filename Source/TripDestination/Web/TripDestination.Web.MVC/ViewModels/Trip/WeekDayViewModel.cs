namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using Common.Infrastructure.Mapping;
    using System;

    public class WeekDayViewModel : IMapFrom<TripDestination.Web.Infrastructure.Models.WeekDay>
    {
        public DateTime Datetime { get; set; }

        public bool IsActive { get; set; }
    }
}