namespace TripDestination.Common.Infrastructure.Models
{
    public class BaseResponseAjaxModel
    {
        public BaseResponseAjaxModel()
            : this(false)
        {
        }

        public BaseResponseAjaxModel(bool status)
        {
            this.Status = status;
        }

        public bool Status { get; set; }

        public object Data { get; set; }

        public string ErrorMessage { get; set; }

        public BaseSignalRModel SignalRModel { get; set; }
    }
}
