namespace TripDestination.Web.MVC.ViewModels.AJAX
{
    using System;

    public class BaseResponseModel
    {
        public BaseResponseModel()
            : this(false)
        {
        }

        public BaseResponseModel(bool status)
        {
            this.Status = status;
        }

        public bool Status { get; set; }

        public object Data { get; set; }
    }
}