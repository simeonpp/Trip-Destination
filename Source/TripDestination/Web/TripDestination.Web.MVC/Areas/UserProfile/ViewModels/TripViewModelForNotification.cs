namespace TripDestination.Web.MVC.Areas.UserProfile.ViewModels
{
    using Common.Infrastructure.Mapping;
    using Data.Models;

    public class TripViewModelForNotification : IMapFrom<Trip>
    {
        public int Id { get; set; }

        public TownViewModel From { get; set; }

        public TownViewModel To { get; set; }

        public string Url
        {
            get
            {
                return string.Format("/Trip/{0}/{1}-{2}", this.Id, this.From.Name, this.To.Name);
            }
        }
    }
}