namespace TripDestination.Tests.Services.Data
{
    using System.Linq;
    using System.Collections.Generic;
    using NUnit.Framework;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Data.Services;
    using Moq;
    using TripDestination.Data.Models;
    using TripDestination.Data.Common;
    [TestFixture]
    public class PageServicesTests
    {
        private const string Heading = "rAnD0m Heading";
        private const string SubHeading = "rAnD0m Subhe@";
        private const int Layout = 86;
        private const int PageId = 435345;

        private IPageServices pageService;

        [SetUp]
        public void Init()
        {
            var pageDbRepositoryMock = new Mock<IDbRepository<Page>>();
            pageDbRepositoryMock.Setup(x => x.All())
                .Returns(new List<Page>()
                {
                    new Page { Id = 435345, Heading = Heading, Layout = Layout, SubHeading = SubHeading }
                }.AsQueryable());

            var paragraphDbRepositoryMock = new Mock<IDbRepository<PageParagraph>>();


            var pageService = new PageServices(pageDbRepositoryMock.Object, paragraphDbRepositoryMock.Object);
            this.pageService = pageService;
        }

        [Test]
        public void GetAllShouldReturnCorrectNumber()
        {
            var actual = this.pageService.GetAll();
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void GetAllShouldReturnCorrectData()
        {
            var actual = this.pageService.GetAll();
            Assert.AreEqual(PageId, actual.FirstOrDefault().Id);
            Assert.AreEqual(Heading, actual.FirstOrDefault().Heading);
            Assert.AreEqual(SubHeading, actual.FirstOrDefault().SubHeading);
            Assert.AreEqual(Layout, actual.FirstOrDefault().Layout);
        }

        [Test]
        public void GetByIdShouldReturnCorrectData()
        {
            var actual = this.pageService.GetById(435345);
            Assert.AreEqual(Heading, actual.Heading);
            Assert.AreEqual(SubHeading, actual.SubHeading);
            Assert.AreEqual(Layout, actual.Layout);
        }
    }
}
