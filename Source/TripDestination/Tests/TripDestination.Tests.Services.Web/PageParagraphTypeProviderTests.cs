namespace TripDestination.Tests.Services.Web
{
    using System.Linq;
    using NUnit.Framework;
    using TripDestination.Services.Web.Providers;
    using TripDestination.Services.Web.Providers.Contracts;
    using Data.Models;
    using System;

    [TestFixture]
    class PageParagraphTypeProviderTests
    {
        [Test]
        public void GetPagePargraphTypesShouldReturnCorrectNumber()
        {
            IPageParagraphTypeProvider tripHelper = new PageParagraphTypeProvider();
            var actual = tripHelper.GetPagePargraphTypes();
            Assert.AreEqual(Enum.GetNames(typeof(PageParagraphType)).Length, actual.Count());
        }
    }
}
