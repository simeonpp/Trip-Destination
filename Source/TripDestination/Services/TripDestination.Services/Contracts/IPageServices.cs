namespace TripDestination.Services.Contracts
{
    using System.Linq;
    using Data.Models;

    public interface IPageServices
    {
        Page GetById(int id);

        IQueryable<PageParagraph> GetParagraphsByPage(Page page);
    }
}
