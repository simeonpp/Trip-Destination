namespace TripDestination.Services.Data
{
    using Contracts;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class ContactFormServices : IContactFormServices
    {
        private readonly IDbRepository<ContactForm> contactFormRepos;

        public ContactFormServices(IDbRepository<ContactForm> contactFormRepos)
        {
            this.contactFormRepos = contactFormRepos;
        }

        public ContactForm Create(string name, string email, string subject, string message, string ip)
        {
            ContactForm dbContactForm = new ContactForm()
            {
                Name = name,
                Email = email,
                Subject = subject,
                Message = message,
                Ip = ip
            };

            this.contactFormRepos.Add(dbContactForm);
            this.contactFormRepos.Save();

            return dbContactForm;
        }

        public void Delete(int id)
        {
            var contactForm = this.contactFormRepos
                .All()
                .Where(c => c.Id == id)
                .FirstOrDefault();

            if (contactForm != null)
            {
                this.contactFormRepos.Delete(contactForm);
                this.contactFormRepos.Save();
            }
        }

        public IQueryable<ContactForm> GetAll()
        {
            return this.contactFormRepos.All();
        }
    }
}
