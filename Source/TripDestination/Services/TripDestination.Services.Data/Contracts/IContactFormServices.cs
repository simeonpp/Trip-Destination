namespace TripDestination.Services.Data.Contracts
{
    using TripDestination.Data.Models;

    public interface IContactFormServices
    {
        ContactForm Create(string name, string email, string subject, string message, string ip);
    }
}
