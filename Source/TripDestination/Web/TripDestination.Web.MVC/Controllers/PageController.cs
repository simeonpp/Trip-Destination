﻿namespace TripDestination.Web.MVC.Controllers
{
    using Ninject;
    using Services.Contracts;
    using System.Web.Mvc;
    using Data.Models;
    using AutoMapper.QueryableExtensions;
    using ViewModels.Page;
    using System.Linq;

    public class PageController : BaseController
    {
        [Inject]
        public IPageServices PageServices { get; set; }

        public ActionResult Index(int id)
        {
            Page page = this.PageServices.GetById(id);

            if (page == null)
            {
                return this.View("Error");
            }

            var paragraphs = this.PageServices
                .GetParagraphsByPage(page)
                .ProjectTo<PageParagraphViewModel>(this.MapperConfiguration)
                .ToList();

            PageViewModel videModel = new PageViewModel()
            {
                Paragraphs = paragraphs
            };
            
            return this.View(videModel);
        }
    }
}