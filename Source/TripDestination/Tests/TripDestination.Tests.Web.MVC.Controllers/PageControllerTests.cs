namespace TripDestination.Tests.Web.MVC.Controllers
{
    using Common.Infrastructure.Mapping;
    using NUnit.Framework;
    using TripDestination.Web.MVC.Controllers;
    using Moq;
    using Services.Data.Contracts;
    using Data.Models;
    using System.Linq;
    using System.Collections.Generic;
    using TestStack.FluentMVCTesting;
    using TripDestination.Web.MVC.ViewModels.Page;

    [TestFixture]
    public class PageControllerTests
    {
        private const int PageId = 1;
        private const string Mainheading = "Main headinggggg";
        private const string MainSubHeading = "Main subheadinggg";
        private const PageParagraphType type = PageParagraphType.FAQ;
        private const string Heading = "Headingggg";
        private const string Text = "Lorem lorem lorem";
        private const string AdditionalHeading = "Additional headinggg";
        private const string AdditionalText = "Additional textttttt";

        private PageController PageController;

        [SetUp]
        public void Init()
        {
            AutoMapperConfig.Register(typeof(PageController).Assembly);

            var pageServices = new Mock<IPageServices>();
            pageServices.Setup(x => x.GetById(PageId))
                .Returns(new Page());
            pageServices.Setup(x => x.GetParagraphsByPage(It.IsAny<Page>()))
                .Returns(new List<PageParagraph>()
                {
                    new PageParagraph()
                    {
                        MainHeading = Mainheading,
                        MainSubHeading = MainSubHeading,
                        Type = type,
                        Heading = Heading,
                        Text = Text,
                        AdditionalHeading = AdditionalHeading,
                        AdditionalText = AdditionalText
                    }
                }.AsQueryable());

            var pageController = new PageController(pageServices.Object);

            pageController.MapperConfiguration = AutoMapperConfig.Configuration;
            this.PageController = pageController;
        }

        [Test]
        public void IndexhouldRenderCorrectView()
        {
            this.PageController.WithCallTo(x => x.Index(PageId))
                .ShouldRenderView("Index")
                .WithModel<PageViewModel>();
        }

        [Test]
        public void IndexShouldRenderCorrectViewWithCorrectParagraphInformation()
        {
            this.PageController.WithCallTo(x => x.Index(PageId))
                .ShouldRenderView("Index")
                .WithModel<PageViewModel>(
                    vm =>
                    {
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().MainHeading, Mainheading);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().MainSubHeading, MainSubHeading);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().Type, type);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().Heading, Heading);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().Text, Text);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().AdditionalHeading, AdditionalHeading);
                        Assert.AreEqual(vm.Paragraphs.FirstOrDefault().AdditionalText, AdditionalText);
                    }
                );
        }

        [Test]
        public void IndexhouldRenderErrorViewWhenNotFoundPageIdIsPassed()
        {
            this.PageController.WithCallTo(x => x.Index(PageId + 1))
                .ShouldRenderView("Error");
        }
    }
}
