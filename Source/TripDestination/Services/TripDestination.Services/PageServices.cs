namespace TripDestination.Data.Services
{
    using System.Linq;

    using Common;
    using Models;
    using TripDestination.Services.Data.Contracts;

    public class PageServices : IPageServices
    {
        private IDbRepository<Page> pageRepos;

        private IDbRepository<PageParagraph> pageParagraphRepos;

        public PageServices(IDbRepository<Page> pageRepos, IDbRepository<PageParagraph> pageParagraphRepos)
        {
            this.pageRepos = pageRepos;
            this.pageParagraphRepos = pageParagraphRepos;
        }

        public Page GetById(int id)
        {
            Page page = this.pageRepos
                .All()
                .Where(p => p.Id == id)
                .FirstOrDefault();

            return page;
        }

        public IQueryable<PageParagraph> GetParagraphsByPage(Page page)
        {
            var paragraphs = this.pageParagraphRepos
                .All()
                .Where(pp => pp.PageId == page.Id);

            return paragraphs;
        }
    }
}
