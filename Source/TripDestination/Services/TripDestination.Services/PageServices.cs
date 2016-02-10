namespace TripDestination.Services
{
    using Contracts;
    using Data.Data.Repositories;
    using Data.Models;
    using System.Linq;
    using System;

    public class PageServices : IPageServices
    {
        private IRepository<Page> pageRepos;

        private IRepository<PageParagraph> pageParagraphRepos;

        public PageServices(IRepository<Page> pageRepos, IRepository<PageParagraph> pageParagraphRepos)
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
