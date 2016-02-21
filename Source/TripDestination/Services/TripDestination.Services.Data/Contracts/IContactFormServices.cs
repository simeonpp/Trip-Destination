namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IContactFormServices
    {
        ContactForm Create(string name, string email, string subject, string message, string ip);

        IQueryable<ContactForm> GetAll();

        void Delete(int id);
    }
}
