namespace TripDestination.Services.Data
{
    using Common.Infrastructure.Models;
    using Contracts;
    using System.Linq;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class NewsletterServices : INewsletterServices
    {
        private IDbRepository<Newsletter> newsletterRepos;

        public NewsletterServices(IDbRepository<Newsletter> newsletterRepos)
        {
            this.newsletterRepos = newsletterRepos;
        }

        public BaseResponseAjaxModel Create(string email, string ip, string userAgent)
        {
            var responseData = new BaseResponseAjaxModel();

            bool emailAlreadyRegistered = this.newsletterRepos
                .All()
                .Where(n => n.Email == email)
                .Any();

            if (emailAlreadyRegistered)
            {
                responseData.ErrorMessage = string.Format("emai '{0}' is already registered for our newsletter.", email);
            }
            else
            {
                Newsletter dbNewsletter = new Newsletter()
                {
                    Email = email,
                    Ip = ip,
                    UserAgent = userAgent
                };

                this.newsletterRepos.Add(dbNewsletter);
                this.newsletterRepos.Save();

                responseData.Status = true;
            }

            return responseData;
        }
    }
}
