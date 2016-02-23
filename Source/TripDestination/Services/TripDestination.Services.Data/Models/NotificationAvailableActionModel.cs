namespace TripDestination.Services.Data.Models
{
    public class NotificationAvailableActionModel
    {
        public bool CanApprove { get; set; }

        public bool CanDisapprove { get; set; }

        public string Url { get; set; }

        public bool HasUrlAction
        {
            get
            {
                return string.IsNullOrEmpty(this.Url);
            }
        }
    }
}
