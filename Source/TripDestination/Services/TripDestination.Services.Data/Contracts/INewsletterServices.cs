namespace TripDestination.Services.Data.Contracts
{
    using Common.Infrastructure.Models;
    using TripDestination.Data.Models;

    public interface INewsletterServices
    {
        BaseResponseAjaxModel Create(string email, string ip, string userAgent);
    }
}
