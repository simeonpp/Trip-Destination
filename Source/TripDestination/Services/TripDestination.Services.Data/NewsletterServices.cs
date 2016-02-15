namespace TripDestination.Services.Data
{
    using Contracts;
    using System;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class NewsletterServices : INewsletterServices
    {
        private IDbRepository<Newsletter> newsletterRepos;

        public NewsletterServices(IDbRepository<Newsletter> newsletterRepos)
        {
            this.newsletterRepos = newsletterRepos;
        }

        public Newsletter Create(string email, string ip, string userAgent)
        {
            Newsletter dbNewsletter = new Newsletter()
            {
                Email = email,
                Ip = ip,
                UserAgent = userAgent
            };

            this.newsletterRepos.Add(dbNewsletter);
            this.newsletterRepos.Save();
            return dbNewsletter;
        }
    }
}
