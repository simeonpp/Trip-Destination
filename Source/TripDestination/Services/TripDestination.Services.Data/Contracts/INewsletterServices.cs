namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using Common.Infrastructure.Models;
    using TripDestination.Data.Models;

    public interface INewsletterServices
    {
        IQueryable<Newsletter> GetAll();

        BaseResponseAjaxModel Create(string email, string ip, string userAgent);

        void Delete(int id);
    }
}
