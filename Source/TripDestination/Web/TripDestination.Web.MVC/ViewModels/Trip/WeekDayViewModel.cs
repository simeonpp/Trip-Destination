namespace TripDestination.Web.MVC.ViewModels.Trip
{
    using System;
    using Common.Infrastructure.Mapping;

    public class WeekDayViewModel : IMapFrom<TripDestination.Web.Infrastructure.Models.WeekDay>
    {
        public DateTime Datetime { get; set; }

        public bool IsActive { get; set; }

        public string FormattedDayOfWeek
        {
            get
            {
                return this.Datetime.DayOfWeek.ToString().Substring(0, 2);
            }
        }

        public string FormattedDateNoTime
        {
            get
            {
                return this.Datetime.ToString("yyyy-MM-dd");
            }
        }
    }
}