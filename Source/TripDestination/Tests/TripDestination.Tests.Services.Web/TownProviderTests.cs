namespace TripDestination.Tests.Services.Web
{
    using System.Linq;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TripDestination.Services.Data.Contracts;
    using TripDestination.Services.Web.Helpers.Contracts;
    using TripDestination.Services.Web.Helpers;
    using System.Web.Mvc;
    using Data.Models;
    using TripDestination.Services.Web.Providers;
    using TripDestination.Services.Web.Providers.Contracts;
    using TripDestination.Services.Web.Services.Contracts;

    [TestFixture]
    public class TownProviderTests
    {
        private ITownProvider townProvider;

        [SetUp]
        public void Init()
        {
            var townServices = new Mock<ITownsServices>();
            townServices.Setup(x => x.GetAll())
                .Returns(new List<Town>()
                {
                    new Town { Name = "Sofia" }
                }.AsQueryable());

            var cacheMock = new Mock<ICacheServices>();

            this.townProvider = new TownProvider(townServices.Object, cacheMock.Object);
        }

        [Test]
        public void GetTownsShouldReturnCorrectNumber()
        {
            var actual = this.townProvider.GetTowns();
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void GetTownsShouldReturnCorrectData()
        {
            var actual = this.townProvider.GetTowns();
            Assert.AreEqual("Sofia", actual.FirstOrDefault().Text);
        }
    }
}
