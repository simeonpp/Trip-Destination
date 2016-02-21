namespace TripDestination.Services.Data
{
    using System.Linq;
    using Contracts;
    using System;
    using TripDestination.Data.Common;
    using TripDestination.Data.Models;

    public class PageParagraphServices : IPageParagraphServices
    {
        private IDbRepository<PageParagraph> pageParagraphRepos;

        private IPageServices pageServices;

        public PageParagraphServices(IDbRepository<PageParagraph> pageParagraphRepos, IPageServices pageServices)
        {
            this.pageParagraphRepos = pageParagraphRepos;
            this.pageServices = pageServices;
        }

        public PageParagraph GetById(int id)
        {
            return this.pageParagraphRepos
                .All()
                .Where(pp => pp.Id == id)
                .FirstOrDefault();
        }

        public IQueryable<PageParagraph> GetAll()
        {
            return this.pageParagraphRepos.All();
        }

        public PageParagraph Create(
            int pageId,
            string mainHeading,
            string mainSubHeading,
            PageParagraphType type,
            string heading,
            string text,
            string additionalHeading,
            string additionalText)
        {
            Page page = this.pageServices.GetById(pageId);

            if (page == null)
            {
                throw new Exception("No such page.");
            }

            PageParagraph pageParagraph = new PageParagraph()
            {
                MainHeading = mainHeading,
                MainSubHeading = mainSubHeading,
                Type = type,
                Heading = heading,
                Text = text,
                AdditionalHeading = additionalHeading,
                AdditionalText = additionalText,
                PageId = pageId
            };

            this.pageParagraphRepos.Add(pageParagraph);
            this.pageParagraphRepos.Save();

            return pageParagraph;
        }

        public PageParagraph Edit(
            int pageParagraphId,
            int pageId,
            string mainHeading,
            string mainSubHeading,
            PageParagraphType type,
            string heading,
            string text,
            string additionalHeading,
            string additionalText)
        {
            PageParagraph pageParagraph = this.GetById(pageParagraphId);
            Page page = this.pageServices.GetById(pageId);

            if (pageParagraph == null || page == null)
            {
                throw new Exception("Page or page paragraph not foun.");
            }

            pageParagraph.MainHeading = mainHeading;
            pageParagraph.MainSubHeading = mainSubHeading;
            pageParagraph.Type = type;
            pageParagraph.Heading = heading;
            pageParagraph.Text = text;
            pageParagraph.AdditionalHeading = additionalHeading;
            pageParagraph.AdditionalText = additionalText;
            pageParagraph.PageId = pageId;

            this.pageParagraphRepos.Save();
            return pageParagraph;
        }

        public void Delete(int id)
        {
            PageParagraph pageParagraph = this.GetById(id);

            if (pageParagraph == null)
            {
                throw new Exception("page paragraph not foun.");
            }

            this.pageParagraphRepos.Delete(pageParagraph);
            this.pageParagraphRepos.Save();
        }
    }
}
