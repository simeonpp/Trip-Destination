namespace TripDestination.Data.Services
{
    using System.Linq;
    using System.Collections.Generic;

    using Common;
    using Models;
    using TripDestination.Services.Data.Contracts;
    using System;

    public class PageServices : IPageServices
    {
        private readonly IDbRepository<Page> pageRepos;

        private readonly IDbRepository<PageParagraph> pageParagraphRepos;

        public PageServices(IDbRepository<Page> pageRepos, IDbRepository<PageParagraph> pageParagraphRepos)
        {
            this.pageRepos = pageRepos;
            this.pageParagraphRepos = pageParagraphRepos;
        }

        public IQueryable<Page> GetAll()
        {
            var pages = this.pageRepos
                .All()
                .OrderBy(p => p.Layout);

            return pages;
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

        public Page Create(string heading, string subHeading, int layout)
        {
            string slug = heading.ToLower().Replace(' ', '-');

            Page page = new Page()
            {
                Heading = heading,
                SubHeading = subHeading,
                Layout = layout,
                Slug = slug
            };

            this.pageRepos.Add(page);
            this.pageRepos.Save();

            return page;
        }

        public Page Edit(int id, string heading, string subHeading, int layout)
        {
            var page = this.GetById(id);

            if (page == null)
            {
                throw new Exception("No such page.");
            }

            page.Heading = heading;
            page.SubHeading = subHeading;
            page.Layout = layout;

            this.pageRepos.Save();
            return page;
        }

        public void Delete(int id)
        {
            var page = this.GetById(id);

            if (page != null)
            {
                this.pageRepos.Delete(page);
                this.pageRepos.Save();
            }
        }
    }
}
