namespace TripDestination.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IPageServices
    {
        IQueryable<Page> GetAll();

        Page GetById(int id);

        IQueryable<PageParagraph> GetParagraphsByPage(Page page);

        Page Create(string heading, string subHeading, int layout);

        Page Edit(int id, string heading, string subHeading, int layout);

        void Delete(int id);
    }
}
