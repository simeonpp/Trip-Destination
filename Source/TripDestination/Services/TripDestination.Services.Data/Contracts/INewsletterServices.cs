namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface INewsletterServices
    {
        Newsletter Create(string email, string ip, string userAgent);
    }
}
