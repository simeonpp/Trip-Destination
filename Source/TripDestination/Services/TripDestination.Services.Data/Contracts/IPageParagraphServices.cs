namespace TripDestination.Services.Data.Contracts
{
    using System.Linq;
    using TripDestination.Data.Models;

    public interface IPageParagraphServices
    {
        PageParagraph GetById(int id);

        IQueryable<PageParagraph> GetAll();

        PageParagraph Create(
            int pageId,
            string mainHeading,
            string mainSubHeading,
            PageParagraphType type,
            string heading,
            string text,
            string additionalHeading,
            string additionalText);

        PageParagraph Edit(
            int pageParagraphId,
            int pageId,
            string mainHeading,
            string mainSubHeading,
            PageParagraphType type,
            string heading,
            string text,
            string additionalHeading,
            string additionalText);

        void Delete(int id);
    }
}
